using System;
using System.Diagnostics;

using nanoFramework.Bluetooth.HID.Services;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public abstract class HidDevice : HIDService, IDisposable
	{
		protected readonly string _deviceName;
		protected readonly BluetoothLEServer _server;

		protected readonly GenericAccessService _genericAccessService;
		protected readonly DeviceInfoService _deviceInfoService;
		protected readonly ScanParamsService _scanParamsService;
		protected readonly BatteryService _batteryService;

		private GattServiceProvider hidDeviceServiceProvider;

		public event EventHandler Connected;
		public event EventHandler Disconnected;

		public HidType Type { get; }

		public bool IsConnected { get; private set; }

		protected HidDevice(string deviceName,
			DeviceInformation deviceInfo,
			HidType hidType,
			ProtocolMode protocolMode,
			PnpElements plugAndPlayElements) : base(protocolMode)
		{
			_deviceName = deviceName;
			_server = BluetoothLEServer.Instance;

			_genericAccessService = new(deviceName, hidType);
			_deviceInfoService = new(deviceInfo, plugAndPlayElements);
			_scanParamsService = new();
			_batteryService = new();

			Type = hidType;
		}

		public override void Initialize()
		{
			_genericAccessService.Initialize();
			_deviceInfoService.Initialize();
			_scanParamsService.Initialize();
			_batteryService.Initialize();

			base.Initialize();
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
			_server.Appearance = (ushort)Type;

			_server.Pairing.AllowBonding = true;
			_server.Pairing.ProtectionLevel = DevicePairingProtectionLevel.Encryption;
			_server.Pairing.IOCapabilities = DevicePairingIOCapabilities.NoInputNoOutput;

			_server.Session.SessionStatusChanged += OnGattSessionChanged;

			_server.Start();
		}

		private void StopBleServer()
		{
			_server.Stop();
		}

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

		public void Dispose()
		{
			StopAdvertising();
			_server.Dispose();
		}
	}
}
