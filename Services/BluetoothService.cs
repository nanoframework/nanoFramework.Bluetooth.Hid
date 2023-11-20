using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth;
using System;

namespace nanoFramework.Bluetooth.HID.Services
{
    internal abstract class BluetoothService
    {
        public abstract void Initialize();

        protected static GattLocalService CreateGattService(Guid serviceUuid)
        {
            var gattServiceProviderResult = GattServiceProvider.Create(serviceUuid);
            if (gattServiceProviderResult.Error != BluetoothError.Success)
            {
                throw new Exception(gattServiceProviderResult.Error.ToString());
            }

            var gattService = gattServiceProviderResult.ServiceProvider.Service;
            return gattService;
        }
    }
}
