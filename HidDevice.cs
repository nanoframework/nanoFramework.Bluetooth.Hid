﻿using System;
using System.Diagnostics;

using nanoFramework.Bluetooth.HID.Services;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID
{
    public abstract class HidDevice : HIDService, IDisposable
    {
        protected readonly string _deviceName;
        protected readonly BluetoothLEServer _server;

        private GattServiceProvider hidDeviceServiceProvider;

        public event EventHandler DeviceConnected;

        protected HidDevice(string deviceName, ProtocolMode protocolMode) : base(protocolMode)
        {
            _deviceName = deviceName;
            _server = BluetoothLEServer.Instance;
        }

        public virtual void Advertise()
        {
            StartBleServer();
            AdvertiseHidService();
        }

        public virtual void StopAdvertising()
        {
            StopAdvertisingHidService();
            StopBleServer();
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

            adParams.Advertisement.LocalName = _deviceName;
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
            _server.DeviceName = _deviceName;
            _server.Appearance = (ushort)HidType.Keyboard;

            _server.Pairing.AllowBonding = true;
            _server.Pairing.ProtectionLevel = DevicePairingProtectionLevel.Encryption;
            _server.Pairing.IOCapabilities = DevicePairingIOCapabilities.NoInputNoOutput;
            _server.Pairing.PairingRequested += OnPairingRequested;
            _server.Pairing.PairingComplete += OnPairingCompleted;

            _server.Start();
        }

        private void StopBleServer()
        {
            _server.Stop();
        }

        public virtual void OnPairingRequested(object sender, DevicePairingRequestedEventArgs args)
        {
            Debug.WriteLine($"Pairing Requested: {args.PairingKind.ToString()} - PIN: {args.Pin.ToString()}");
            args.Accept();
            Debug.WriteLine($"Pairing Accepted.");
        }

        public virtual void OnPairingCompleted(object sender, DevicePairingEventArgs args)
        {
            Debug.WriteLine($"Pairing Completed: {args.Status.ToString()}");

            DeviceConnected?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            StopAdvertising();
            _server.Dispose();
        }
    }
}