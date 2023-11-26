using System;
using System.Threading;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Bluetooth.HID.Services;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID
{
    public class Keyboard : HidDevice
    {
        private readonly GenericAccessService _genericAccessService;
        private readonly DeviceInfoService _deviceInfoService;
        private readonly ScanParamsService _scanParamsService;
        private readonly BatteryService _batteryService;

        private GattLocalCharacteristic _inputReportCharacteristic;

        public Keyboard(
            string deviceName,
            DeviceInformation deviceInfo,
            ProtocolMode protocolMode,
            PnpElements plugAndPlayElements
            ) : base(deviceName, protocolMode)
        {
            _genericAccessService = new(deviceName, HidType.Keyboard);
            _deviceInfoService = new(deviceInfo, plugAndPlayElements);
            _scanParamsService = new();
            _batteryService = new();
        }

        public override void Initialize()
        {
            _genericAccessService.Initialize();
            _deviceInfoService.Initialize();
            _scanParamsService.Initialize();
            _batteryService.Initialize();

            base.Initialize();
        }

        public void Do()
        {
            var report = new byte[] { 0b0000_1000, 0x00, /**/ 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00 };
            _inputReportCharacteristic.NotifyValue(new (report));

            Thread.Sleep(100);

            // release
            report = new byte[8];
            _inputReportCharacteristic.NotifyValue(new(report));
        }

        protected override void CreateReportMapCharacteristic()
        {
            var reportMap = new byte[]
            {
                // KEYBOARD REPORT
                0x05, 0x01,  // Usage Pg (Generic Desktop)
                0x09, 0x06,  // Usage (Keyboard)
                0xA1, 0x01,  // Collection: (Application)
                0x85, 0x02,  // Report Id (2)
                0x05, 0x07,  //   Usage Pg (Key Codes)
                0x19, 0xE0,  //   Usage Min (224)
                0x29, 0xE7,  //   Usage Max (231)
                0x15, 0x00,  //   Log Min (0)
                0x25, 0x01,  //   Log Max (1)
                //   Modifier byte
                0x75, 0x01,  //   Report Size (1)
                0x95, 0x08,  //   Report Count (8)
                0x81, 0x02,  //   Input: (Data, Variable, Absolute)
                //   Reserved byte
                0x95, 0x01,  //   Report Count (1)
                0x75, 0x08,  //   Report Size (8)
                0x81, 0x01,  //   Input: (Constant)
                //   LED report
                0x95, 0x05,  //   Report Count (5)
                0x75, 0x01,  //   Report Size (1)
                0x05, 0x08,  //   Usage Pg (LEDs)
                0x19, 0x01,  //   Usage Min (1)
                0x29, 0x05,  //   Usage Max (5)
                0x91, 0x02,  //   Output: (Data, Variable, Absolute)
                //   LED report padding
                0x95, 0x01,  //   Report Count (1)
                0x75, 0x03,  //   Report Size (3)
                0x91, 0x01,  //   Output: (Constant)
                //   Key arrays (6 bytes)
                0x95, 0x06,  //   Report Count (6)
                0x75, 0x08,  //   Report Size (8)
                0x15, 0x00,  //   Log Min (0)
                0x25, 0x65,  //   Log Max (101)
                0x05, 0x07,  //   Usage Pg (Key Codes)
                0x19, 0x00,  //   Usage Min (0)
                0x29, 0x65,  //   Usage Max (101)
                0x81, 0x00,  //   Input: (Data, Array)
                0xC0,        // End Collection
            };

            var reportMapCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.ReportMap, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = new Buffer(reportMap),
            });

            if (reportMapCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportMapCharacteristicResult.Error.ToString());
            }

            var externalReportReferenceDescriptorResult = reportMapCharacteristicResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10503), new()
            {
                StaticValue = (new Buffer(new byte[] { 0x00, 0x00 }))
            });

            if (externalReportReferenceDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(externalReportReferenceDescriptorResult.Error.ToString());
            }
        }

        protected override void CreateReportCharacteristics()
        {
            CreateInputReportCharacteristic();

            // TODO: add other report types, namely the output report for LEDs
        }

        private void CreateInputReportCharacteristic()
        {
            var inputReportCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.Report, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }).ToBuffer()
            });

            if (inputReportCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(inputReportCharacteristicResult.Error.ToString());
            }

            var clientConfigDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(GattDescriptorUuids.ClientCharacteristicConfiguration, new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x00 }).ToBuffer() // no notification | 0x0001 enable notification | 0x0002 enable indication
            });

            if (clientConfigDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(clientConfigDescriptorResult.Error.ToString());
            }

            var reportRefDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10504), new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x02, 0x01 }).ToBuffer() // report id + report type
            });

            if (reportRefDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportRefDescriptorResult.Error.ToString());
            }

            _inputReportCharacteristic = inputReportCharacteristicResult.Characteristic;
        }
    }
}
