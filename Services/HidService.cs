

using Bluetooth.Characteristics;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using System;
using System.Diagnostics;

namespace Bluetooth.Services
{
    public class HidService
    {
        private const ushort HumanInterfaceDeviceId = 0x1812;
        private const ushort HidInformationId = 0x2A4A;
        private const ushort HidControlPointId = 0x2A4C;
        private const ushort ReportMapId = 0x2A4B;
        private const ushort ProtocolModeId = 0x2A4E;

        private readonly GattLocalService _hidService;

        public HidService(GattServiceProvider provider)
        {
            _hidService = provider.AddService(Utilities.CreateUuidFromShortCode(HumanInterfaceDeviceId));
            //_hidService = provider.AddService(new Guid("A7EEDF2C-DA8C-4CB5-A9C5-5151C78B0057"));

            // Information Country and flag
            var hidInformation = _hidService.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(HidInformationId),
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = new Buffer(new byte[] { 0x01, 0x11, 0x00, 0x01 })
                });

            // Set portmap
            var write = new DataWriter();
            write.WriteBytes(ReportMap.Mouse);
            var reportMap = _hidService.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(ReportMapId),
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    StaticValue = write.DetachBuffer(),
                });

            write = new DataWriter();
            write.WriteUInt16(0x0000);
            reportMap.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(0x2907), new GattLocalDescriptorParameters()
            {
                ReadProtectionLevel = GattProtectionLevel.Plain,
                StaticValue = write.DetachBuffer(),
            });

            var controlPoint = _hidService.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(HidControlPointId),
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse,
                });

            controlPoint.Characteristic.ReadRequested += Characteristic_ReadRequested;

            // Set protocol mode as fully standard
            write = new DataWriter();
            write.WriteByte(0x01);
            var protocolMode = _hidService.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(ProtocolModeId),
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse | GattCharacteristicProperties.Read,
                    StaticValue = write.DetachBuffer(),
                });

            var inputReport = new InputReportCharacteristic(_hidService, 0);
            inputReport.Input.ReadRequested += Input_ReadRequested;
        }

        private void Input_ReadRequested(GattLocalCharacteristic sender, GattReadRequestedEventArgs ReadRequestEventArgs)
        {
            Debug.WriteLine("A request to read input");
        }

        private void Characteristic_ReadRequested(GattLocalCharacteristic sender, GattReadRequestedEventArgs ReadRequestEventArgs)
        {
            Debug.WriteLine("A request to read");
        }
    }
}
