
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth;
using System;

namespace Bluetooth.Characteristics
{
    public class OutputReportCharacteristic
    {
        public GattLocalCharacteristic Output { get; }
        public OutputReportCharacteristic(GattLocalService provider, byte reportId)
        {
            var outputReport = provider.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(0x2A4D),
                new GattLocalCharacteristicParameters()
                {
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Write | GattCharacteristicProperties.WriteWithoutResponse
                }
                );

            Output = outputReport.Characteristic;

            DataWriter write = new DataWriter();
            write.WriteBytes(new byte[] { reportId, 0x02 });
            Output.CreateDescriptor(
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
