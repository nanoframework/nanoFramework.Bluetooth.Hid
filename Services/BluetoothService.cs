// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.Hid.Services
{
    /// <summary>
    /// Base class for a Bluetooth Service.
    /// </summary>
    public abstract class BluetoothService
    {
        /// <summary>
        /// Initializes the current instance of the <see cref="BluetoothService"/>.
        /// This will create all the required characteristics and descriptors on the bluetooth stack.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Attempts to get the specified service from the Bluetooth LE Server or create
        /// a new one if it does not exist.
        /// </summary>
        /// <param name="serviceUuid">The Service ID to get or create.</param>
        /// <returns>An instance of <see cref="GattLocalService"/> representing the specified service.</returns>
        /// <exception cref="Exception">A bluetooth error. More information in the exception message property.</exception>
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
