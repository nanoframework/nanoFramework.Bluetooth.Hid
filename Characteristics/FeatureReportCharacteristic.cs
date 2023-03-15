
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth;
using System;

namespace Bluetooth.Characteristics
{
    public class FeatureReportCharacteristic
    {
        public GattLocalCharacteristic Feature { get; }
        public FeatureReportCharacteristic(GattLocalService provider, byte reportId)
        {
            var featureReport = provider.CreateCharacteristic(
                Utilities.CreateUuidFromShortCode(0x2A4D),
                new GattLocalCharacteristicParameters()
                {
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Write,
                }
                );

            Feature = featureReport.Characteristic;

            DataWriter write = new DataWriter();
            write.WriteBytes(new byte[] { reportId, 0x03 });
            Feature.CreateDescriptor(
                Utilities.CreateUuidFromShortCode(0x2908),
                new GattLocalDescriptorParameters()
                {
                    ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
                    StaticValue = write.DetachBuffer(),
                }
                );
        }
    }
}
