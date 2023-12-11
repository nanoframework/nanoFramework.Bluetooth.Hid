// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.HID.Devices
{
    public class MouseInputReport
    {
        public byte Button { get; private set; }

        public sbyte X { get; private set; }

        public sbyte Y { get; private set; }

        public sbyte Wheel { get; private set; }

        public void Press(MouseButton button)
        {
            Button = (byte)button;
        }

        public void Release()
        {
            Button = 0x00;
        }

        public void Move(sbyte x, sbyte y)
        {
            X = x;
            Y = y;
        }

        public void Scroll(sbyte wheelScroll)
        {
            Wheel = wheelScroll;
        }

        public void Reset()
        {
            Button = 0;
            X = 0;
            Y = 0;
            Wheel = 0;
        }

        public byte[] ToBytes()
        {
            return new byte[4]
            {
                Button,
                (byte)X,
                (byte)Y,
                (byte)Wheel
            };
        }
    }
}
