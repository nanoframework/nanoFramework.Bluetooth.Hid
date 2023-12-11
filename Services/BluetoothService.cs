// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Services
{
    public abstract class BluetoothService
    {
        public abstract void Initialize();

        protected static GattLocalService CreateOrGetGattService(Guid serviceUuid)
        {
            var existingService = BluetoothLEServer.Instance.GetServiceByUUID(serviceUuid);
            if (existingService != null)
            {
                return existingService.Service;
            }

            var gattServiceProviderResult = GattServiceProvider.Create(serviceUuid);
            if (gattServiceProviderResult.Error != BluetoothError.Success)
            {
                throw new Exception(gattServiceProviderResult.Error.ToString());
            }

            var gattService = gattServiceProviderResult.ServiceProvider.Service;
            return gattService;
        }
    }
}
