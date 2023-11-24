using System;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    internal sealed class ScanParamsService : BluetoothService
    {
        private GattLocalCharacteristic scanRefreshCharacteristic;

        public ushort ScanInterval { get; private set; }

        public ushort ScanWindow { get; private set; }

        public override void Initialize()
        {
            var gattService = CreateGattService(GattServiceUuids.ScanParameters);

            CreateScanIntervalWindowCharacteristic(gattService);
            CreateScanRefreshCharacteristic(gattService);
        }

        public void RequestRefresh()
        {
            if (scanRefreshCharacteristic == null)
            {
                throw new InvalidOperationException();
            }

            scanRefreshCharacteristic.NotifyValue(new Buffer(new byte[1] { 0x00 }));
        }

        private void CreateScanIntervalWindowCharacteristic(GattLocalService gattService)
        {
            var result = gattService.CreateCharacteristic(GattCharacteristicUuids.ScanIntervalWindow, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse,
                UserDescription = "Scan Interval Window"
            });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }

            result.Characteristic.WriteRequested += OnScanIntervalWindowCharacteristicWriteRequested;
        }

        private void OnScanIntervalWindowCharacteristicWriteRequested(GattLocalCharacteristic sender, GattWriteRequestedEventArgs writeRequestEventArgs)
        {
            var value = writeRequestEventArgs.GetRequest().Value;
            var dr = DataReader.FromBuffer(value);

            this.ScanInterval = dr.ReadUInt16();
            this.ScanWindow = dr.ReadUInt16();
        }

        private void CreateScanRefreshCharacteristic(GattLocalService gattService)
        {
            var result = gattService.CreateCharacteristic(GattCharacteristicUuids.ScanRefresh, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Notify,
                UserDescription = "Scan Refresh"
            });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }

            scanRefreshCharacteristic = result.Characteristic;
        }
    }
}
