using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    public sealed class GenericAccessService : BluetoothService
    {
        public string DeviceName { get; }

        public HidType Appearance { get; }

        public GenericAccessService(string deviceName, HidType appearance)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException();
            }

            DeviceName = deviceName;
            Appearance = appearance;
        }

        public override void Initialize()
        {
            var gattService = CreateOrGetGattService(GattServiceUuids.GenericAccess);

            CreateGapDeviceNameCharacteristic(gattService);
            CreateGapAppearanceCharacteristic(gattService);
        }

        private void CreateGapDeviceNameCharacteristic(GattLocalService gattService)
        {
            var deviceNameCharacteristicResult = gattService.CreateCharacteristic(GattCharacteristicUuids.GapDeviceName, new()
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
            var appearanceCharacteristicResult = gattService.CreateCharacteristic(GattCharacteristicUuids.GapAppearance, new()
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
