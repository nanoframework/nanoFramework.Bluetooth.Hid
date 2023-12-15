// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.Hid.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using UnitsNet;

namespace nanoFramework.Bluetooth.Hid.Services
{
    /// <summary>
    /// The Bluetooth LE battery service.
    /// </summary>
    public class BatteryService : BluetoothService
    {
        private GattLocalCharacteristic _batteryLevelCharacteristic;

        private bool _disableSerivce;
        private Ratio _batteryLevel;

        /// <summary>
        /// Gets or sets the battery level to report to the HID Host.
        /// </summary>
        public Ratio BatteryLevel
        {
            get => _batteryLevel;
            set
            {
                if (_disableSerivce || _batteryLevel.Value != value.Value)
                {
                    var byteVal = (byte)value.Percent;
                    _batteryLevelCharacteristic.NotifyValue(byteVal.AsBuffer());

                    _batteryLevel = value;
                    _disableSerivce = false;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatteryService"/> class.
        /// </summary>
        public BatteryService()
        {
            _batteryLevel = new Ratio(100, UnitsNet.Units.RatioUnit.Percent);
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
            => _disableSerivce = false;

        /// <summary>
        /// Disable reporting battery levels to the HID Host.
        /// </summary>
        public void Disable()
            => _disableSerivce = true;

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

            _batteryLevelCharacteristic = characteristicResult.Characteristic;
            _batteryLevelCharacteristic.ReadRequested += OnBatteryLevelReadRequest;
        }

        private void OnBatteryLevelReadRequest(
            GattLocalCharacteristic sender,
            GattReadRequestedEventArgs readRequestEventArgs)
        {
            var request = readRequestEventArgs.GetRequest();
            if (_disableSerivce)
            {
                request.RespondWithProtocolError((byte)BluetoothError.DisabledByPolicy);
            }
            else
            {
                request.RespondWithValue(((byte)_batteryLevel.Percent).AsBuffer());
            }
        }
    }
}
