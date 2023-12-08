using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth;
using System;

namespace nanoFramework.Bluetooth.HID.Devices
{
    public class Mouse : HidDevice
    {
        public Mouse(
            string deviceName,
            ProtocolMode protocolMode
            ) : base(deviceName, protocolMode)
        {
        }

        protected override void CreateReportCharacteristics()
        {
            throw new System.NotImplementedException();
        }

        protected override void CreateReportMapCharacteristic()
        {
            var reportMap = new byte[] {
            0x05, 0x01,  // Usage Page (Generic Desktop)
            0x09, 0x02,  // Usage (Mouse)
            0xA1, 0x01,  // Collection (Application)
            0x85, 0x01,  // Report Id (1)
            0x09, 0x01,  //   Usage (Pointer)
            0xA1, 0x00,  //   Collection (Physical)
            0x05, 0x09,  //     Usage Page (Buttons)
            0x19, 0x01,  //     Usage Minimum (01) - Button 1
            0x29, 0x03,  //     Usage Maximum (03) - Button 3
            0x15, 0x00,  //     Logical Minimum (0)
            0x25, 0x01,  //     Logical Maximum (1)
            0x75, 0x01,  //     Report Size (1)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x02,  //     Input (Data, Variable, Absolute) - Button states
            0x75, 0x05,  //     Report Size (5)
            0x95, 0x01,  //     Report Count (1)
            0x81, 0x01,  //     Input (Constant) - Padding or Reserved bits
            0x05, 0x01,  //     Usage Page (Generic Desktop)
            0x09, 0x30,  //     Usage (X)
            0x09, 0x31,  //     Usage (Y)
            0x09, 0x38,  //     Usage (Wheel)
            0x15, 0x81,  //     Logical Minimum (-127)
            0x25, 0x7F,  //     Logical Maximum (127)
            0x75, 0x08,  //     Report Size (8)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x06,  //     Input (Data, Variable, Relative) - X coordinate, Y coordinate, wheel
            0xC0,        //   End Collection
            0xC0,        // End Collection
            };

            var reportMapCharacteristicResult = HidGattService.CreateCharacteristic(
                GattCharacteristicUuids.ReportMap,
                new()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Read,
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    StaticValue = reportMap.AsBuffer(),
                });

            if (reportMapCharacteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(reportMapCharacteristicResult.Error.ToString());
            }

            var externalReportReferenceDescriptorResult = reportMapCharacteristicResult
                .Characteristic
                .CreateDescriptor(
                    Utilities.CreateUuidFromShortCode(10503),
                    new()
                    {
                        StaticValue = (new byte[] { 0x00, 0x00 }).AsBuffer()
                    });

            if (externalReportReferenceDescriptorResult.Error != BluetoothError.Success)
            {
                throw new Exception(externalReportReferenceDescriptorResult.Error.ToString());
            }
        }
    }
}
