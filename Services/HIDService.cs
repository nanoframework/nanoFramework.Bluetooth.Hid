using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    public abstract class HIDService : BluetoothService
    {
        private readonly ProtocolMode _protocolMode;

        protected GattLocalService HidGattService;

        public event HidHostStateChangedEventHandler HidHostStateChanged;

        public HIDService(ProtocolMode protocolMode)
        {
            _protocolMode = protocolMode;
        }

        public override void Initialize()
        {
            HidGattService = CreateGattService(GattServiceUuids.HumanInterfaceDevice);

            CreateProtocolModeCharacteristic();

            CreateReportCharacteristics();
            CreateReportMapCharacteristic();

            CreateHidInformationCharacteristic();
            CreateHidControlPointCharacteristic();
        }

        protected virtual void CreateHidInformationCharacteristic()
        {
            var hidInfo = new byte[]
            {
                0x01, // bcdHID v01
                0x11, // bcdHID (contd) .11
                0x00, // bCountryCode (0x00 = Not Localized)
                0x02  // flags (bit 0: remote wake 0 = false / bit 1: normally connectable (bool))
            };

            var result = HidGattService.CreateCharacteristic(GattCharacteristicUuids.HidInformation, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read,
                StaticValue = hidInfo.AsBuffer()
            });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }

        protected abstract void CreateReportMapCharacteristic();

        protected abstract void CreateReportCharacteristics();

        private void CreateProtocolModeCharacteristic()
        {
            var result = HidGattService.CreateCharacteristic(GattCharacteristicUuids.ProtocolMode, new()
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
            var hidControlPointCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.HidControlPoint, new()
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
