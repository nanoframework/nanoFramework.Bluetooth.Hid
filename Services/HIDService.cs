// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    /// <summary>
    /// Base class for HID Services.
    /// </summary>
    public abstract class HIDService : BluetoothService
    {
        private readonly ProtocolMode _protocolMode;

        /// <summary>
        /// Gets or sets the HID GATT Bluetooth Service.
        /// </summary>
        protected GattLocalService HidGattService { get; set; }

        /// <summary>
        /// HID Host State Changed event handler.
        /// </summary>
        /// <param name="sender">The sender object instance.</param>
        /// <param name="args">The event arguments.</param>
        public delegate void HidHostStateChangedEventHandler(object sender, HidHostStateArgs args);

        /// <summary>
        /// Occurs when the HID has changed its state.
        /// </summary>
        public event HidHostStateChangedEventHandler HidHostStateChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="HIDService"/> class.
        /// </summary>
        /// <param name="protocolMode">The report mode to use for this device.</param>
        public HIDService(ProtocolMode protocolMode)
        {
            _protocolMode = protocolMode;
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            HidGattService = CreateOrGetGattService(GattServiceUuids.HumanInterfaceDevice);

            CreateProtocolModeCharacteristic();

            CreateReportCharacteristics();
            CreateReportMapCharacteristic();

            CreateHidInformationCharacteristic();
            CreateHidControlPointCharacteristic();
        }

        /// <summary>
        /// Create the required HID information bluetooth characteristic.
        /// </summary>
        /// <exception cref="Exception">Bluetooth error.</exception>
        protected virtual void CreateHidInformationCharacteristic()
        {
            var hidInfo = new byte[]
            {
                0x01, // bcdHID v01
                0x11, // bcdHID (contd) .11
                0x00, // bCountryCode (0x00 = Not Localized)
                0x02  // flags (bit 0: remote wake 0 = false / bit 1: normally connectable (bool))
            };

            var result = HidGattService.CreateCharacteristic(
                GattCharacteristicUuids.HidInformation,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = hidInfo.AsBuffer()
                });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }

        /// <summary>
        /// Creates the report map characteristic for this device.
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown if there was an error from the bluetooth stack. 
        /// Check the exception message property for the error type.
        /// </exception>
        protected abstract void CreateReportMapCharacteristic();

        /// <summary>
        /// Creates the report characteristic needed for this device.
        /// </summary>
        protected abstract void CreateReportCharacteristics();

        private void CreateProtocolModeCharacteristic()
        {
            var result = HidGattService.CreateCharacteristic(
                GattCharacteristicUuids.ProtocolMode,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = (new byte[1] { (byte)_protocolMode }).AsBuffer()
                });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }

        private void CreateHidControlPointCharacteristic()
        {
            var hidControlPointCharacteristicResult = HidGattService.CreateCharacteristic(
                GattCharacteristicUuids.HidControlPoint,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse,
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                });

            if (hidControlPointCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(hidControlPointCharacteristicResult.Error.ToString());
            }

            hidControlPointCharacteristicResult.Characteristic.WriteRequested += (sender, args) =>
            {
                var writeRequest = args.GetRequest();
                var flag = writeRequest.Value.AsByte();

                HidHostStateChanged?.Invoke(this, new HidHostStateArgs(flag));
            };
        }
    }
}
