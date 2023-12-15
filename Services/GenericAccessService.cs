// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    /// <summary>
    /// The Bluetooth LE Generic Access Service.
    /// </summary>
    public sealed class GenericAccessService : BluetoothService
    {
        /// <summary>
        /// Gets the device name.
        /// </summary>
        public string DeviceName { get; }

        /// <summary>
        /// Gets the device appearance.
        /// </summary>
        public HidType Appearance { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericAccessService"/> class.
        /// </summary>
        /// <param name="deviceName">The device name.</param>
        /// <param name="appearance">The device appearance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="deviceName"/> cannot be null.</exception>
        public GenericAccessService(string deviceName, HidType appearance)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException();
            }

            DeviceName = deviceName;
            Appearance = appearance;
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            var gattService = CreateOrGetGattService(GattServiceUuids.GenericAccess);

            CreateGapDeviceNameCharacteristic(gattService);
            CreateGapAppearanceCharacteristic(gattService);
        }

        private void CreateGapDeviceNameCharacteristic(GattLocalService gattService)
        {
            var deviceNameCharacteristicResult = gattService.CreateCharacteristic(
                GattCharacteristicUuids.GapDeviceName,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = DeviceName.ToBuffer()
                });

            if (deviceNameCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(deviceNameCharacteristicResult.Error.ToString());
            }
        }

        private void CreateGapAppearanceCharacteristic(GattLocalService gattService)
        {
            var appearanceCharacteristicResult = gattService.CreateCharacteristic(
                GattCharacteristicUuids.GapAppearance,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = ((ushort)Appearance).ToBuffer()
                });

            if (appearanceCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(appearanceCharacteristicResult.Error.ToString());
            }
        }
    }
}
