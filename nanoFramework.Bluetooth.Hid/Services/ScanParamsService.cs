// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.Hid.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.Hid.Services
{
    /// <summary>
    /// The Bluetooth LE scan parameters service.
    /// </summary>
    public class ScanParamsService : BluetoothService
    {
        private GattLocalCharacteristic _scanRefreshCharacteristic;

        /// <summary>
        /// Gets the scan interval value.
        /// </summary>
        public ushort ScanInterval { get; private set; }

        /// <summary>
        /// Gets the scan window value.
        /// </summary>
        public ushort ScanWindow { get; private set; }

        /// <inheritdoc/>
        public override void Initialize()
        {
            var gattService = CreateOrGetGattService(GattServiceUuids.ScanParameters);

            CreateScanIntervalWindowCharacteristic(gattService);
            CreateScanRefreshCharacteristic(gattService);
        }

        /// <summary>
        /// Request the HID Host to refresh the bluetooth scan parameters.
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Initialize"/> must be called first.</exception>
        public void RequestRefresh()
        {
            if (_scanRefreshCharacteristic == null)
            {
                throw new InvalidOperationException();
            }

            _scanRefreshCharacteristic.NotifyValue((new byte[1] { 0x00 }).AsBuffer());
        }

        private void CreateScanIntervalWindowCharacteristic(GattLocalService gattService)
        {
            var result = gattService.CreateCharacteristic(
                GattCharacteristicUuids.ScanIntervalWindow,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.WriteWithoutResponse,
                    UserDescription = "Scan Interval Window"
                });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }

            result.Characteristic.WriteRequested += OnScanIntervalWindowCharacteristicWriteRequested;
        }

        private void OnScanIntervalWindowCharacteristicWriteRequested(GattLocalCharacteristic sender, GattWriteRequestedEventArgs writeRequestEventArgs)
        {
            try
            {
                var value = writeRequestEventArgs.GetRequest().Value;
                var dr = DataReader.FromBuffer(value);

                this.ScanInterval = dr.ReadUInt16();
                this.ScanWindow = dr.ReadUInt16();
            }
            catch
            {
                // do nothing
            }
        }

        private void CreateScanRefreshCharacteristic(GattLocalService gattService)
        {
            var result = gattService.CreateCharacteristic(
                GattCharacteristicUuids.ScanRefresh,
                new GattLocalCharacteristicParameters()
                {
                    CharacteristicProperties = GattCharacteristicProperties.Notify,
                    UserDescription = "Scan Refresh"
                });

            if (result.Error != BluetoothError.Success)
            {
                throw new Exception(result.Error.ToString());
            }

            _scanRefreshCharacteristic = result.Characteristic;
        }
    }
}
