// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Devices
{
    /// <summary>
    /// A generic Bluetooth Low-Energy Keyboard Implementation.
    /// </summary>
    public class Keyboard : HidDevice
    {
        private const byte MaxPressedKeyCount = 0x06;

        private readonly KeyboardInputReport _report;

        private GattLocalCharacteristic _inputReport;
        private GattLocalCharacteristic _outputReport;
        private TimeSpan _keyPressDelay;

        /// <summary>
        /// Gets the status of the LED device as reported by the OS.
        /// </summary>
        public LedStatus LedStatus { get; }

        /// <summary>
        /// LED Status Changed event handler.
        /// </summary>
        /// <param name="sender">The sender object instance.</param>
        /// <param name="ledStatus">The LED Status report instance.</param>
        public delegate void LedStatusChangedEventHandler(object sender, LedStatus ledStatus);

        /// <summary>
        /// Occurs when the <see cref="LedStatus"/> value has changed.
        /// </summary>
        public event LedStatusChangedEventHandler LedStatusChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Keyboard"/> class.
        /// </summary>
        /// <param name="deviceName">The device name. Used when advertising the device.</param>
        /// <param name="deviceInfo">Additional device information.</param>
        /// <param name="protocolMode">The protocol mode to use when communicating with the HID Host. Use <see cref="ProtocolMode.Report"/> if unsure.</param>
        /// <param name="plugAndPlayElements">Plug and Play information.</param>
        public Keyboard(
            string deviceName,
            DeviceInformation deviceInfo,
            ProtocolMode protocolMode,
            PnpElements plugAndPlayElements
            ) : base(deviceName, deviceInfo, HidType.Keyboard, protocolMode, plugAndPlayElements)
        {
            _report = new(maxNumPressedKey: 6);
            _keyPressDelay = TimeSpan.FromMilliseconds(150);

            LedStatus = new();
        }

        /// <summary>
        /// Simulates pressing a keyboard key.
        /// </summary>
        /// <param name="key">The key to press. Use the <see cref="Keys"/> for a complete set of supported keys.</param>
        public void Press(byte key)
        {
            _report.AddKey(key);
            SendInputReport();
        }

        /// <summary>
        /// Simulates one or more key presses and releases.
        /// </summary>
        /// <param name="keys">The keys to send. Use the <see cref="Keys"/> for a complete set of supported keys.</param>
        /// <exception cref="ArgumentNullException"><paramref name="keys"/> is null.</exception>
        public void Send(params byte[] keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var key in keys)
            {
                _report.AddKey(key);
            }

            SendInputReport();

            _report.Reset();
            SendInputReport();
        }

        /// <summary>
        /// Releases a pressed key.
        /// </summary>
        /// <param name="key">The key to press. Use the <see cref="Keys"/> for a complete set of supported keys.</param>
        public void Release(byte key)
        {
            _report.RemoveKey(key);
            SendInputReport();
        }

        /// <summary>
        /// Releases all currently pressed keys.
        /// </summary>
        public void ReleaseAll()
        {
            _report.Reset();
            SendInputReport();
        }

        /// <summary>
        /// Sets how long a key would remain pressed when using <see cref="Press(byte)"/> and <see cref="Send(byte[])"/>.
        /// </summary>
        /// <param name="keyPressDelay">The amount of time to hold down a key when using <see cref="Press(byte)"/> and <see cref="Send(byte[])"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetKeyPressDelay(TimeSpan keyPressDelay)
        {
            if (keyPressDelay == TimeSpan.Zero
                || keyPressDelay == TimeSpan.MinValue
                || keyPressDelay == TimeSpan.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            _keyPressDelay = keyPressDelay;
        }

        /// <inheritdoc/>
        protected override void CreateReportMapCharacteristic()
        {
            var reportMap = new byte[]
            {
                // KEYBOARD REPORT
                0x05, 0x01,                 //// Usage Pg (Generic Desktop)
                0x09, 0x06,                 //// Usage (Keyboard)
                0xA1, 0x01,                 //// Collection: (Application)
                0x85, 0x01,                 //// Report Id (1)
                0x05, 0x07,                 ////   Usage Pg (Key Codes)
                0x19, 0xE0,                 ////   Usage Min (224)
                0x29, 0xE7,                 ////   Usage Max (231)
                0x15, 0x00,                 ////   Log Min (0)
                0x25, 0x01,                 ////   Log Max (1)
                //   Modifier byte
                0x75, 0x01,                 ////   Report Size (1)
                0x95, 0x08,                 ////   Report Count (8)
                0x81, 0x02,                 ////   Input: (Data, Variable, Absolute)
                //   Reserved byte
                0x95, 0x01,                 ////   Report Count (1)
                0x75, 0x08,                 ////   Report Size (8)
                0x81, 0x01,                 ////   Input: (Constant)
                //   LED report
                0x95, 0x05,                 ////   Report Count (5)
                0x75, 0x01,                 ////   Report Size (1)
                0x05, 0x08,                 ////   Usage Pg (LEDs)
                0x19, 0x01,                 ////   Usage Min (1)
                0x29, 0x05,                 ////   Usage Max (5)
                0x91, 0x02,                 ////   Output: (Data, Variable, Absolute)
                //   LED report padding
                0x95, 0x01,                 ////   Report Count (1)
                0x75, 0x03,                 ////   Report Size (3)
                0x91, 0x01,                 ////   Output: (Constant)
                //   Key arrays (6 bytes)
                0x95, MaxPressedKeyCount,   ////   Report Count (6)
                0x75, 0x08,                 ////   Report Size (8)
                0x15, 0x00,                 ////   Log Min (0)
                0x25, 0x65,                 ////   Log Max (101)
                0x05, 0x07,                 ////   Usage Pg (Key Codes)
                0x19, 0x00,                 ////   Usage Min (0)
                0x29, 0x65,                 ////   Usage Max (101)
                0x81, 0x00,                 ////   Input: (Data, Array)
                0xC0,                       //// End Collection
            };

            var reportMapCharacteristicResult = HidGattService.CreateCharacteristic(
                GattCharacteristicUuids.ReportMap,
                new()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    StaticValue = reportMap.AsBuffer(),
                });

            if (reportMapCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportMapCharacteristicResult.Error.ToString());
            }

            var externalReportReferenceDescriptorResult = reportMapCharacteristicResult
                .Characteristic
                .CreateDescriptor(
                    Utilities.CreateUuidFromShortCode(10503),
                    new()
                    {
                        StaticValue = (new byte[] { 0x00, 0x00 }).AsBuffer()
                    });

            if (externalReportReferenceDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(externalReportReferenceDescriptorResult.Error.ToString());
            }
        }

        /// <inheritdoc/>
        protected override void CreateReportCharacteristics()
        {
            CreateInputReportCharacteristic();
            CreateOutputReportCharacteristic();
        }

        private void CreateInputReportCharacteristic()
        {
            var inputReportCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.Report, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = _report.ToBytes().AsBuffer()
            });

            if (inputReportCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(inputReportCharacteristicResult.Error.ToString());
            }

            var clientConfigDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(GattDescriptorUuids.ClientCharacteristicConfiguration, new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x00 }).AsBuffer() // no notification | 0x0001 enable notification | 0x0002 enable indication
            });

            if (clientConfigDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(clientConfigDescriptorResult.Error.ToString());
            }

            var reportRefDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10504), new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x01, 0x01 }).AsBuffer() // report id + report type
            });

            if (reportRefDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportRefDescriptorResult.Error.ToString());
            }

            _inputReport = inputReportCharacteristicResult.Characteristic;
        }

        private void CreateOutputReportCharacteristic()
        {
            var outputReportCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.Report, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Write | GattCharacteristicProperties.WriteWithoutResponse,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired
            });

            if (outputReportCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(outputReportCharacteristicResult.Error.ToString());
            }

            var reportRefDescriptorResult = outputReportCharacteristicResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10504), new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x02 }).AsBuffer() // report id + report type
            });

            if (reportRefDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportRefDescriptorResult.Error.ToString());
            }

            _outputReport = outputReportCharacteristicResult.Characteristic;
            _outputReport.WriteRequested += OnOutputReportReceived;
        }

        private void OnOutputReportReceived(GattLocalCharacteristic sender, GattWriteRequestedEventArgs writeRequestEventArgs)
        {
            try
            {
                var val = writeRequestEventArgs.GetRequest().Value;
                if (val.Length == 0)
                {
                    return;
                }

                var dataReader = DataReader.FromBuffer(val);
                var bytes = new byte[val.Length];

                dataReader.ReadBytes(bytes);

                var ledStatus = bytes[bytes.Length - 1];

                LedStatus.IsNumLockOn = (ledStatus & 0x01) == 0x01;
                LedStatus.IsCapsLockOn = (ledStatus & 0x02) == 0x02;
                LedStatus.IsSrollLockOn = (ledStatus & 0x04) == 0x04;

                LedStatusChanged?.Invoke(this, LedStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Sleep()
        {
            Thread.Sleep(_keyPressDelay);
        }

        private void SendInputReport()
        {
            _inputReport.NotifyValue(_report.ToBytes().AsBuffer());
            Sleep();
        }
    }
}
