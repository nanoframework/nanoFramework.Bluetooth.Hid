using System;

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;
using nanoFramework.Device.Bluetooth.GenericAttributeProfile;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public class Mouse : HidDevice
	{
		private readonly MouseInputReport _report;

		private GattLocalCharacteristic _inputReport;

		public Mouse(
			string deviceName,
			DeviceInformation deviceInfo,
			ProtocolMode protocolMode,
			PnpElements plugAndPlayElements) : base(deviceName, deviceInfo, protocolMode, plugAndPlayElements)
		{
			_report = new();
		}

		public void Click(MouseButton button)
		{
			_report.Reset();

			_report.Press(button);
			SendInputReport();

			_report.Release();
			SendInputReport();
		}

		public void Move(sbyte x, sbyte y)
		{
			_report.Move(x, y);
			SendInputReport();
		}

		public void Scroll(sbyte scollPosition)
		{
			_report.Scroll(scollPosition);
			SendInputReport();
		}

		protected override void CreateReportMapCharacteristic()
		{
			var reportMap = new byte[] {
			0x05, 0x01,  // Usage Page (Generic Desktop)
            0x09, 0x02,  // Usage (Mouse)
            0xA1, 0x01,  // Collection (Application)
            0x85, 0x01,  // Report Id (1)
            0x09, 0x01,  //   Usage (Pointer)
            0xA1, 0x00,  //   Collection (Physical)
            0x05, 0x09,  //     Usage Page (Buttons)
            0x19, 0x01,  //     Usage Minimum (01) - Button 1
            0x29, 0x03,  //     Usage Maximum (03) - Button 3
            0x15, 0x00,  //     Logical Minimum (0)
            0x25, 0x01,  //     Logical Maximum (1)
            0x75, 0x01,  //     Report Size (1)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x02,  //     Input (Data, Variable, Absolute) - Button states
            0x75, 0x05,  //     Report Size (5)
            0x95, 0x01,  //     Report Count (1)
            0x81, 0x01,  //     Input (Constant) - Padding or Reserved bits
            0x05, 0x01,  //     Usage Page (Generic Desktop)
            0x09, 0x30,  //     Usage (X)
            0x09, 0x31,  //     Usage (Y)
            0x09, 0x38,  //     Usage (Wheel)
            0x15, 0x81,  //     Logical Minimum (-127)
            0x25, 0x7F,  //     Logical Maximum (127)
            0x75, 0x08,  //     Report Size (8)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x06,  //     Input (Data, Variable, Relative) - X coordinate, Y coordinate, wheel
            0xC0,        //   End Collection
            0xC0,        // End Collection
            };

			var reportMapCharacteristicResult = HidGattService.CreateCharacteristic(
				GattCharacteristicUuids.ReportMap,
				new()
				{
					CharacteristicProperties = GattCharacteristicProperties.Read,
					ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
					StaticValue = reportMap.AsBuffer(),
				});

			if (reportMapCharacteristicResult.Error != BluetoothError.Success)
			{
				throw new Exception(reportMapCharacteristicResult.Error.ToString());
			}

			var externalReportReferenceDescriptorResult = reportMapCharacteristicResult
				.Characteristic
				.CreateDescriptor(
					Utilities.CreateUuidFromShortCode(10503),
					new()
					{
						StaticValue = (new byte[] { 0x00, 0x00 }).AsBuffer()
					});

			if (externalReportReferenceDescriptorResult.Error != BluetoothError.Success)
			{
				throw new Exception(externalReportReferenceDescriptorResult.Error.ToString());
			}
		}

		protected override void CreateReportCharacteristics()
		{
			CreateInputReportCharacteristic();
		}

		private void CreateInputReportCharacteristic()
		{
			var inputReportCharacteristicResult = HidGattService.CreateCharacteristic(GattCharacteristicUuids.Report, new()
			{
				CharacteristicProperties = GattCharacteristicProperties.Read | GattCharacteristicProperties.Notify,
				ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
				WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
				StaticValue = _report.ToBytes().AsBuffer()
			});

			if (inputReportCharacteristicResult.Error != BluetoothError.Success)
			{
				throw new Exception(inputReportCharacteristicResult.Error.ToString());
			}

			var clientConfigDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(GattDescriptorUuids.ClientCharacteristicConfiguration, new()
			{
				ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
				WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
				StaticValue = (new byte[] { 0x00, 0x00 }).AsBuffer() // no notification | 0x0001 enable notification | 0x0002 enable indication
			});

			if (clientConfigDescriptorResult.Error != BluetoothError.Success)
			{
				throw new Exception(clientConfigDescriptorResult.Error.ToString());
			}

			var reportRefDescriptorResult = inputReportCharacteristicResult.Characteristic.CreateDescriptor(Utilities.CreateUuidFromShortCode(10504), new()
			{
				ReadProtectionLevel = GattProtectionLevel.EncryptionRequired,
				WriteProtectionLevel = GattProtectionLevel.EncryptionRequired,
				StaticValue = (new byte[] { 0x01, 0x01 }).AsBuffer() // report id + report type
			});

			if (reportRefDescriptorResult.Error != BluetoothError.Success)
			{
				throw new Exception(reportRefDescriptorResult.Error.ToString());
			}

			_inputReport = inputReportCharacteristicResult.Characteristic;
		}

		private void SendInputReport()
		{
			_inputReport.NotifyValue(_report.ToBytes().AsBuffer());
		}
	}
}
