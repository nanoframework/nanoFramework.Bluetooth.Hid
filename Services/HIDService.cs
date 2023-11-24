using System;

using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    internal sealed class HIDService : BluetoothService
    {
        private GattLocalCharacteristic reportCharacteristic;

        public override void Initialize()
        {
            var gattService = CreateGattService(GattServiceUuids.HumanInterfaceDevice);

            CreateProtocolModeCharacteristic(gattService);
        }

        private static void CreateProtocolModeCharacteristic(GattLocalService gattService)
        {
            throw new NotImplementedException();
        }
    }
}
