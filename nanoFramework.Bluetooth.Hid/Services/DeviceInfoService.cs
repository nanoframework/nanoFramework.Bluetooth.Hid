// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.Hid.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.Hid.Services
{
    /// <summary>
    /// The Bluetooth LE Device Information Service.
    /// </summary>
    public class DeviceInfoService : BluetoothService
    {
        /// <summary>
        /// Gets the <see cref="DeviceInformation"/> instance containing
        /// the information to report to the HID Host.
        /// </summary>
        public DeviceInformation DeviceInformation { get; }

        /// <summary>
        /// Gets the <see cref="PnpElements"/> instance containing
        /// Plug and Play information.
        /// </summary>
        public PnpElements PnpElements { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfoService"/> class.
        /// </summary>
        /// <param name="deviceInformation">The device information.</param>
        /// <param name="pnpElements">The plug and play information.</param>
        /// <exception cref="ArgumentNullException"><paramref name="deviceInformation"/> cannot be null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="pnpElements"/> cannot be null.</exception>
        public DeviceInfoService(
            DeviceInformation deviceInformation,
            PnpElements pnpElements)
        {
            DeviceInformation = deviceInformation ?? throw new ArgumentNullException();
            PnpElements = pnpElements ?? throw new ArgumentNullException();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            var gattService = CreateOrGetGattService(GattServiceUuids.DeviceInformation);

            // set up device info characteristic
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.ManufacturerNameString, DeviceInformation.Manufacturer);
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.ModelNumberString, DeviceInformation.ModelNumber);
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.SerialNumberString, DeviceInformation.SerialNumber);
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.HardwareRevisionString, DeviceInformation.HardwareRevision);
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.FirmwareRevisionString, DeviceInformation.FirmwareRevision);
            CreateReadStaticCharacteristic(gattService, GattCharacteristicUuids.SoftwareRevisionString, DeviceInformation.SoftwareRevision);

            // set up plug & play
            CreatePnpIdCharacteristic(gattService, PnpElements);
        }

        private static void CreateReadStaticCharacteristic(GattLocalService gattService, Guid uuid, string data)
        {
            if (data == null)
            {
                return;
            }

            var result = gattService.CreateCharacteristic(
                uuid,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = data.ToBuffer()
                });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }

        private static void CreatePnpIdCharacteristic(GattLocalService gattService, PnpElements pnpElement)
        {
            var pnpResult = gattService.CreateCharacteristic(
                GattCharacteristicUuids.PnpId,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = pnpElement.ToBuffer()
                });

            if (pnpResult.Error != BluetoothError.Success)
            {
                throw new Exception(pnpResult.Error.ToString());
            }
        }
    }
}
