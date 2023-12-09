using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    public sealed class DeviceInfoService : BluetoothService
    {
        public DeviceInformation DeviceInformation { get; }

        public PnpElements PnpElements { get; }

        public DeviceInfoService(DeviceInformation deviceInformation,
            PnpElements pnpElements)
        {
            DeviceInformation = deviceInformation ?? throw new ArgumentNullException();
            PnpElements = pnpElements ?? throw new ArgumentNullException();
        }

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

        private static void CreateReadStaticCharacteristic(GattLocalService gattService, Guid Uuid, string data)
        {
            if (data == null)
            {
                return;
            }

            var result = gattService.CreateCharacteristic(Uuid, new GattLocalCharacteristicParameters()
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
            var pnpResult = gattService.CreateCharacteristic(GattCharacteristicUuids.PnpId, new()
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
