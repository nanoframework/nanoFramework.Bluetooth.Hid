using System;
using System.Threading;

using nanoFramework.Bluetooth.HID;
using nanoFramework.Bluetooth.HID.Devices;

namespace BLEKeyboardSample
{
	public class Program
	{
		private static Keyboard kbd;

		public static void Main()
		{
			kbd = new Keyboard(deviceName: "nF BLE Keyboard",
				deviceInfo: new DeviceInformation("nF", "BLEKBD1", "1", "01", "01", "01"),
				protocolMode: ProtocolMode.Report,
				plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));

			kbd.LedStatusChanged += OnLedChanged;

			kbd.Initialize();
			kbd.Advertise();

			while (true)
			{
				if (!kbd.IsConnected)
				{
					Thread.Sleep(2500);
					continue;
				}

				Thread.Sleep(1000);

				//kbd.Press(KeyMap.LeftGUI);
				//kbd.Press(KeyMap.L);

				//kbd.ReleaseAll();
			}
		}

		private static void OnLedChanged(object sender, LedStatus e)
		{
			Console.WriteLine($"LED Changed: CAPSLCK: {e.IsCapsLockOn} | NUMLCK: {e.IsNumLockOn} | SCRLK: {e.IsSrollLockOn}");
		}
	}
}
