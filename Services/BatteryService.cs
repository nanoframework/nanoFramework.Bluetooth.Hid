// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using UnitsNet;

namespace nanoFramework.Bluetooth.HID.Services
{
    /// <summary>
    /// The Bluetooth LE battery service.
    /// </summary>
    public sealed class BatteryService : BluetoothService
    {
        private GattLocalCharacteristic batteryLevelCharacteristic;

        private bool disableSerivce;
        private Ratio batteryLevel;

        /// <summary>
        /// Gets or sets the battery level to report to the HID Host.
        /// </summary>
        public Ratio BatteryLevel
        {
            get => batteryLevel;
            set
            {
                if (disableSerivce || batteryLevel.Value != value.Value)
                {
                    var byteVal = (byte)value.Percent;
                    batteryLevelCharacteristic.NotifyValue(byteVal.AsBuffer());

                    batteryLevel = value;
                    disableSerivce = false;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatteryService"/> class.
        /// </summary>
        public BatteryService()
        {
            batteryLevel = new Ratio(100, UnitsNet.Units.RatioUnit.Percent);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            var gattService = CreateOrGetGattService(GattServiceUuids.Battery);
            CreateBatteryLevelCharacteristic(gattService);
        }

        /// <summary>
        /// Enable reporting battery levels to the HID Host.
        /// </summary>
        public void Enable()
            => disableSerivce = false;

        /// <summary>
        /// Disable reporting battery levels to the HID Host.
        /// </summary>
        public void Disable()
            => disableSerivce = true;

        private void CreateBatteryLevelCharacteristic(GattLocalService gattService)
        {
            var characteristicResult = gattService.CreateCharacteristic(
                GattCharacteristicUuids.BatteryLevel,
                new GattLocalCharacteristicParameters()
                {
                    UserDescription = "Battery Level %",
                    CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify
                });

            if (characteristicResult.Error != BluetoothError.Success)
            {
                throw new Exception(characteristicResult.Error.ToString());
            }

            batteryLevelCharacteristic = characteristicResult.Characteristic;
            batteryLevelCharacteristic.ReadRequested += OnBatteryLevelReadRequest;
        }

        private void OnBatteryLevelReadRequest(
            GattLocalCharacteristic sender,
            GattReadRequestedEventArgs readRequestEventArgs)
        {
            var request = readRequestEventArgs.GetRequest();
            if (disableSerivce)
            {
                request.RespondWithProtocolError((byte)BluetoothError.DisabledByPolicy);
            }
            else
            {
                request.RespondWithValue(((byte)batteryLevel.Percent).AsBuffer());
            }
        }
    }
}
