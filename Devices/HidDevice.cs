// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

using nanoFramework.Bluetooth.HID.Services;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

using UnitsNet;

namespace nanoFramework.Bluetooth.HID.Devices
{
    /// <summary>
    /// HID Device Base Class.
    /// </summary>
    public abstract class HidDevice : HIDService, IDisposable
    {
        private GattServiceProvider hidDeviceServiceProvider;

        /// <summary>
        /// The device name.
        /// </summary>
        protected readonly string DeviceName;

        /// <summary>
        /// The Bluetooth LE Server.
        /// </summary>
        protected readonly BluetoothLEServer Server;

        /// <summary>
        /// The bluetooth generic access service.
        /// </summary>
        protected readonly GenericAccessService GenericAccessService;

        /// <summary>
        /// The bluetooth device information service.
        /// </summary>
        protected readonly DeviceInfoService DeviceInfoService;

        /// <summary>
        /// The bluetooth scan parameters service.
        /// </summary>
        protected readonly ScanParamsService ScanParamsService;

        /// <summary>
        /// The bluetooth battery service.
        /// </summary>
        protected readonly BatteryService BatteryService;

        /// <summary>
        /// Occurs when the device is connected to a HID Host.
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Occurs when the device is disconnected from a HID Host.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Gets the type of the HID Device.
        /// </summary>
        public HidType Type { get; }

        /// <summary>
        /// Gets a value indicating whether if the device is currently connected to a HID Host.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets or sets the current battery level of the device to report to the HID Host.
        /// </summary>
        public Ratio BatteryLevel
        {
            get => BatteryService.BatteryLevel;
            set => BatteryService.BatteryLevel = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HidDevice"/> class.
        /// </summary>
        /// <param name="deviceName">The name to use when advertising the device.</param>
        /// <param name="deviceInfo">The device information.</param>
        /// <param name="hidType">The type of the HID Device. Affects the appearance of the device on HID Hosts.</param>
        /// <param name="protocolMode">The protocol mode to use.</param>
        /// <param name="plugAndPlayElements">Plug and Play information.</param>
        protected HidDevice(
            string deviceName,
            DeviceInformation deviceInfo,
            HidType hidType,
            ProtocolMode protocolMode,
            PnpElements plugAndPlayElements) : base(protocolMode)
        {
            this.DeviceName = deviceName;
            Server = BluetoothLEServer.Instance;

            GenericAccessService = new GenericAccessService(deviceName, hidType);
            DeviceInfoService = new DeviceInfoService(deviceInfo, plugAndPlayElements);
            ScanParamsService = new ScanParamsService();
            BatteryService = new BatteryService();

            Type = hidType;
        }

        /// <summary>
        /// Initializes the HID Device by setting up all required bluetooth services.
        /// </summary>
        public override void Initialize()
        {
            GenericAccessService.Initialize();
            DeviceInfoService.Initialize();
            ScanParamsService.Initialize();
            BatteryService.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// Begins advertising the HID Device to nearby BLE HID hosts.
        /// </summary>
        public virtual void Advertise()
        {
            StartBleServer();
            AdvertiseHidService();
        }

        /// <summary>
        /// Stops advertising the device to nearby BLE HID Hosts.
        /// </summary>
        public virtual void StopAdvertising()
        {
            StopAdvertisingHidService();
            StopBleServer();
        }

        /// <summary>
        /// Enable the battery level reporting capability.
        /// </summary>
        public virtual void EnableBattery()
        {
            BatteryService.Enable();
        }

        /// <summary>
        /// Disable the battery level reporting capability.
        /// </summary>
        public virtual void DisableBattery()
        {
            BatteryService.Disable();
        }

        private void AdvertiseHidService()
        {
            hidDeviceServiceProvider = BluetoothLEServer.Instance.GetServiceByUUID(GattServiceUuids.HumanInterfaceDevice);
            if (hidDeviceServiceProvider == null)
            {
                throw new InvalidOperationException();
            }

            var adParams = new GattServiceProviderAdvertisingParameters
            {
                IsConnectable = true,
                IsDiscoverable = true,
            };

            adParams.Advertisement.LocalName = DeviceName;
            hidDeviceServiceProvider.StartAdvertising(adParams);
        }

        private void StopAdvertisingHidService()
        {
            if (hidDeviceServiceProvider == null)
            {
                return;
            }

            hidDeviceServiceProvider.StopAdvertising();
        }

        private void StartBleServer()
        {
            Server.DeviceName = DeviceName;
            Server.Appearance = (ushort)Type;

            Server.Pairing.AllowBonding = true;
            Server.Pairing.ProtectionLevel = DevicePairingProtectionLevel.Encryption;
            Server.Pairing.IOCapabilities = DevicePairingIOCapabilities.NoInputNoOutput;

            Server.Session.SessionStatusChanged += OnGattSessionChanged;

            Server.Start();
        }

        private void StopBleServer()
        {
            Server.Stop();
        }

        /// <summary>
        /// Handler for the <see cref="GattSession.SessionStatusChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        protected virtual void OnGattSessionChanged(object sender, GattSessionStatusChangedEventArgs args)
        {
            if (args.Status == GattSessionStatus.Active)
            {
                IsConnected = true;
                Connected?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                IsConnected = false;
                Disconnected?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            StopAdvertising();
            Server.Dispose();
        }
    }
}
