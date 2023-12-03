using System;
using System.Collections;

namespace nanoFramework.Bluetooth.HID.Devices
{
    public class KeyReport
    {
        public byte Modifiers { get; set; }

        public byte ReservedByte { get; set; }

        public byte[] Keys { get; private set; }

        public KeyReport()
        {
            Keys = new byte[6];
        }

        public void Reset()
        {
            Modifiers = 0;
            ReservedByte = 0;
            Keys = new byte[6];
        }

        public byte[] ToBytes()
        {
            var result = new byte[8];

            result[0] = Modifiers;
            result[1] = ReservedByte;

            Array.Copy(
                sourceArray: Keys,
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 2,
                length: 6);

            return result;
        }
    }
}
