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
                deviceInfo: new DeviceInformation("nF", "BLEKBD1", "1", "01", "01", "01"),
                protocolMode: ProtocolMode.Report,
                plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));

            //kbd.DeviceConnected += Keyboard_DeviceConnected;

            kbd.Initialize();
            kbd.Advertise();

            while (true)
            {
                Thread.Sleep(1000);
                kbd.LockPC();
            }

            //Thread.Sleep(Timeout.Infinite);
        }

        //private static void Keyboard_DeviceConnected(object sender, System.EventArgs e)
        //{
        //    Thread.Sleep(5000);

        //    kbd.LockPC();
        //}
    }
}
