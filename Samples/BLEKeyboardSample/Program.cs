using System.Threading;

using nanoFramework.Bluetooth.HID;

namespace BLEKeyboardSample
{
    public class Program
    {
        private static Keyboard kbd;

        public static void Main()
        {
            kbd = new Keyboard(deviceName: "nF BLE Keyboard",
                deviceInfo: new DeviceInformation("nF", "BLEKBD1", "1", "0.1", "0.1", "0.1"),
                protocolMode: ProtocolMode.Report,
                plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));

            kbd.DeviceConnected += Keyboard_DeviceConnected;

            kbd.Advertise();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void Keyboard_DeviceConnected(object sender, System.EventArgs e)
        {
            kbd.Do();
        }
    }
}
