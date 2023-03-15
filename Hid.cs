using nanoFramework.Runtime.Native;
using System;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;
using nanoFramework.Device.Bluetooth.Services;
using System.Diagnostics;
using Bluetooth.Enums;
using Bluetooth.Services;

namespace Bluetooth
{
    public class Hid
    {
        public void Initialize()
        {
            Debug.WriteLine("Hello HID");

            //The GattServiceProvider is used to create and advertise the primary service definition.
            //An extra device information service will be automatically created.
            GattServiceProviderResult result = GattServiceProvider.Create(Utilities.CreateUuidFromShortCode(0x1812));

            GattServiceProvider serviceProvider = result.ServiceProvider;

            HidService hid = new HidService(serviceProvider);

            // Add standard Bluetooth Sig services to the provider. These are an example of standard services
            // that can be reused/updated for other applications. Based on standards but simplified. 

            // === Device Information Service ===
            // https://www.bluetooth.com/specifications/specs/device-information-service-1-1/
            // The Device Information Service is created automatically when you create the initial primary service.
            // The default version just has a Manufacturer name of "nanoFramework and model or "Esp32"
            // You can add your own service which will replace the default one.
            // To make it easy we have included some standard services classes to this sample
            DeviceInformationServiceService DifService = new(
                    serviceProvider,
                    "ESP32 BLE Keyboard",
                    new PnpElements() { Sig = 0x02, Vid = 0xE502, Pid = 0xA111, Version = 0x210, },
                    "Super model",
                    null, // no serial number
                    "v1.0",
                    SystemInfo.Version.ToString(),
                    "none");

            // === Battery Service ===
            // https://www.bluetooth.com/specifications/specs/battery-service-1-0/
            // Battery service exposes the current battery level percentage
            BatteryService BatService = new(serviceProvider);

            // Update the Battery service the current battery level regularly. In this case 94%
            BatService.BatteryLevel = 94;

            serviceProvider.StartAdvertising(new GattServiceProviderAdvertisingParameters()
            {
                DeviceName = "BLE top keyboard",
                IsConnectable = true,
                IsDiscoverable = true,
            });

            // Get created Primary service from provider
            ////var service = serviceProvider.AddService(GattServiceUuids.GenericAccess);

            ////DataWriter write = new DataWriter();
            ////write.WriteUInt16((ushort)HidType.Mouse);
            ////var carApp = service.CreateCharacteristic(
            ////    GattCharacteristicUuids.GapAppearance,
            ////    new GattLocalCharacteristicParameters()
            ////    {
            ////        CharacteristicProperties = GattCharacteristicProperties.Read,
            ////        StaticValue = write.DetachBuffer()
            ////    });

            ////write = new DataWriter();
            ////write.WriteString("This one is from here");
            ////var carName = service.CreateCharacteristic(
            ////    GattCharacteristicUuids.GapDeviceName,
            ////    new GattLocalCharacteristicParameters()
            ////    {
            ////        CharacteristicProperties = GattCharacteristicProperties.Read,
            ////        StaticValue = write.DetachBuffer()
            ////    });

        }

        private void Characteristic_ReadRequested(GattLocalCharacteristic sender, GattReadRequestedEventArgs ReadRequestEventArgs)
        {
            Debug.WriteLine($"UUID: {sender.Uuid.ToString()}");
            Debug.WriteLine($"ReadProtectionLevel: {sender.ReadProtectionLevel}");
            Debug.WriteLine($"Num descriptors: {sender.Descriptors.Length}");
            Debug.WriteLine($"{ReadRequestEventArgs.Session.DeviceId.Id}");
        }
    }
}
