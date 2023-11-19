using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    internal class GenericAccessService : IBluetoothService
    {
        public string DeviceName { get; }

        public HidType Appearance { get; }

        public GenericAccessService(string deviceName, HidType appearance)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException();
            }

            this.DeviceName = deviceName;
            this.Appearance = appearance;
        }

        public void Initialize()
        {
            var gattServiceProviderResult = GattServiceProvider.Create(GattServiceUuids.GenericAccess);
            if (gattServiceProviderResult.Error != BluetoothError.Success)
            {
                throw new Exception(gattServiceProviderResult.Error.ToString());
            }

            var gattService = gattServiceProviderResult.ServiceProvider.Service;

            var deviceNameCharacteristicResult = gattService.CreateCharacteristic(GattCharacteristicUuids.GapDeviceName, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read,
                StaticValue = DeviceName.AsBuffer()
            });
            if (deviceNameCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(deviceNameCharacteristicResult.Error.ToString());
            }


        }
    }
}
