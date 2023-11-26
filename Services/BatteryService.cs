using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using UnitsNet;

namespace nanoFramework.Bluetooth.HID.Services
{
    public sealed class BatteryService : BluetoothService
    {
        private GattLocalCharacteristic batteryLevelCharacteristic;

        private bool disableSerivce;
        private Ratio batteryLevel;

        public Ratio BatteryLevel
        {
            get => batteryLevel;
            set
            {
                if (batteryLevel.Value != value.Value)
                {
                    var byteVal = (byte)value.Percent;
                    batteryLevelCharacteristic.NotifyValue(byteVal.ToBuffer());

                    batteryLevel = value;
                    disableSerivce = false;
                }
            }
        }

        public BatteryService()
        {
            batteryLevel = new Ratio(100, UnitsNet.Units.RatioUnit.Percent);
        }

        public override void Initialize()
        {
            var gattService = CreateGattService(GattServiceUuids.Battery);
            CreateBatteryLevelCharacteristic(gattService);
        }

        public void Enable()
            => disableSerivce = false;

        public void Disable()
            => disableSerivce = true;

        private void CreateBatteryLevelCharacteristic(GattLocalService gattService)
        {
            var characteristicResult = gattService.CreateCharacteristic(GattCharacteristicUuids.BatteryLevel, new()
            {
                UserDescription = "Battery Level %",
                CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify
            });

            if (characteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(characteristicResult.Error.ToString());
            }

            batteryLevelCharacteristic = characteristicResult.Characteristic;
            batteryLevelCharacteristic.ReadRequested += OnBatteryLevelReadRequest;
        }

        private void OnBatteryLevelReadRequest(GattLocalCharacteristic sender,
            GattReadRequestedEventArgs readRequestEventArgs)
        {
            var request = readRequestEventArgs.GetRequest();
            if (disableSerivce)
            {
                request.RespondWithProtocolError((byte)BluetoothError.DisabledByPolicy);
            }
            else
            {
                request.RespondWithValue(((byte)batteryLevel.Percent).ToBuffer());
            }
        }
    }
}
