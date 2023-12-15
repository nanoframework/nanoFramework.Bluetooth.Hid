// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading;

using nanoFramework.Bluetooth.Hid;
using nanoFramework.Bluetooth.Hid.Devices;

namespace BLEMouseSample
{
    public class Program
    {
        public static void Main()
        {
            var mouse = new Mouse("nF BLE Mouse",
                deviceInfo: new DeviceInformation("nF", "BLEMOUSE1", "1", "01", "01", "01"),
                protocolMode: ProtocolMode.Report,
                plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));

            mouse.Initialize();
            mouse.Advertise();

            while (true)
            {
                if (!mouse.IsConnected)
                {
                    Thread.Sleep(100);
                    continue;
                }

                Thread.Sleep(1000);

                // move to the right and bottom (diagonal)
                mouse.Move(x: 5, y: 5);

                // scroll down
                mouse.Scroll(-5);

                // left click something
                mouse.Click(MouseButton.Left);
            }
        }
    }
}
