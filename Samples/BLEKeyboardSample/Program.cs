// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
                    Thread.Sleep(100);
                    continue;
                }

                Thread.Sleep(5000);

                /*
                 * On Windows, the following example will:
                 *
                 * - Open the 'Run' Dialog.
                 * - Type 'Notepad'.
                 * - Press Enter to launch notepad.exe.
                 * - Waits for notepad to open.
                 * - Type a warm Hello World message ;)
                 */

                kbd.Send(Keys.Modifiers.LeftGUI, Keys.Alphabet.R);
                KeyboardUtilities.TypeText(kbd, "Notepad");
                kbd.Send(Keys.Control.Return);

                // wait a bit for notepad to launch
                Thread.Sleep(1000);

                KeyboardUtilities.TypeText(kbd, "Hello, World. I want to play a game.");
            }
        }

        private static void OnLedChanged(object sender, LedStatus e)
        {
            Console.WriteLine($"LED Changed: CAPSLCK: {e.IsCapsLockOn} | NUMLCK: {e.IsNumLockOn} | SCRLK: {e.IsSrollLockOn}");
        }
    }
}
