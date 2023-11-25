using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    internal sealed class HIDService : BluetoothService
    {
        private readonly ProtocolMode _protocolMode;
        private readonly byte reportId;

        private GattLocalCharacteristic reportCharacteristic;

        public event HidHostStateChangedEventHandler HidHostStateChanged;

        public HIDService(ProtocolMode protocolMode, byte reportId)
        {
            _protocolMode = protocolMode;
            this.reportId = reportId;
        }

        public override void Initialize()
        {
            var gattService = CreateGattService(GattServiceUuids.HumanInterfaceDevice);

            CreateProtocolModeCharacteristic(gattService, _protocolMode);
            CreateHidControlPointCharacteristic(gattService);
            CreateHidInformationCharacteristic(gattService);

            reportCharacteristic = CreateReportCharacteristic(gattService, reportId);
        }

        private static void CreateProtocolModeCharacteristic(GattLocalService gattService, ProtocolMode protocolMode)
        {
            var result = gattService.CreateCharacteristic(GattCharacteristicUuids.ProtocolMode, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read,
                StaticValue = new Buffer(new byte[1] { (byte)protocolMode })
            });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }

        private static GattLocalCharacteristic CreateReportCharacteristic(GattLocalService gattService, byte reportId)
        {
            // TODO: support the output report too for LEDs in keyboards for example.


            var reportResult = gattService.CreateCharacteristic(GattCharacteristicUuids.Report, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }).ToBuffer()
            });

            if (reportResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportResult.Error.ToString());
            }

            var clientConfigDescriptorResult = reportResult.Characteristic.CreateDescriptor(GattDescriptorUuids.ClientCharacteristicConfiguration, new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { 0x00, 0x00 }).ToBuffer() // no notification | 0x0001 enable notification | 0x0002 enable indication
            });

            if (clientConfigDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(clientConfigDescriptorResult.Error.ToString());
            }

            var reportRefDescriptorResult = reportResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10504), new()
            {
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                StaticValue = (new byte[] { reportId, 0x01 }).ToBuffer() // report id + report type
            });

            if (reportRefDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportRefDescriptorResult.Error.ToString());
            }

            return reportResult.Characteristic;
        }

        private void CreateHidControlPointCharacteristic(GattLocalService gattService)
        {
            var hidControlCharacteristicResult = gattService.CreateCharacteristic(GattCharacteristicUuids.HidControlPoint, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse,
                ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
            });

            if (hidControlCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(hidControlCharacteristicResult.Error.ToString());
            }

            hidControlCharacteristicResult.Characteristic.WriteRequested += (sender, args) =>
            {
                var writeRequest = args.GetRequest();
                var flag = writeRequest.Value.AsByte();

                HidHostStateChanged?.Invoke(this, new HidHostStateArgs(flag));
            };
        }

        private static void CreateHidInformationCharacteristic(GattLocalService gattService)
        {
            var hidInfo = new byte[]
            {
                0x01, // bcdHID v01
                0x11, // bcdHID (contd) .11
                0x00, // bCountryCode (0x00 = Not Localized)
                0x02  // flags (bit 0: remote wake 0 = false / bit 1: normally connectable (bool))
            };

            var result = gattService.CreateCharacteristic(GattCharacteristicUuids.HidInformation, new()
            {
                CharacteristicProperties = GattCharacteristicProperties.Read,
                StaticValue = hidInfo.ToBuffer()
            });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }
        }
    }
}
