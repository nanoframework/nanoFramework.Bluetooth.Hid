
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth;
using System;

namespace Bluetooth.Characteristics
{
    public class InputReportCharacteristic
    {
        public GattLocalCharacteristic Input { get; }

        public InputReportCharacteristic(GattLocalService provider, byte reportId)
        {
            var inputReport = provider.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(0x2A4D),
                new GattLocalCharacteristicParameters()
                {
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify,
                }
                );

            Input = inputReport.Characteristic;            

            DataWriter write = new DataWriter();
            write.WriteBytes(new byte[] { reportId, 0x01 });
            Input.CreateDescriptor(
                Utilities.CreateUuidFromShortCode(0x2908),
                new GattLocalDescriptorParameters()
                {
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    StaticValue = write.DetachBuffer(),
                }
                );
        }
    }
}
