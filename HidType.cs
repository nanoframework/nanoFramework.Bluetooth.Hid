// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.HID
{
    /// <summary>
    /// The type of the HID Device. Affects the appearance of the device in the HID Host.
    /// </summary>
    public enum HidType : ushort
    {
        /// <summary>Generic HID..</summary>
        Generic = 0x03C0,

        /// <summary>Keyboard..</summary>
        Keyboard = 0x03C1,

        /// <summary>Mouse..</summary>
        Mouse = 0x03C2,

        /// <summary>Joystick..</summary>
        Joystick = 0x03C3,

        /// <summary>Gamepad.</summary>
        Gamepad = 0x03C4,

        /// <summary>Tablet..</summary>
        Tablet = 0x03C5,

        /// <summary>Card reader..</summary>
        CardReader = 0x03C6,

        /// <summary>Digital pen..</summary>
        DigitalPen = 0x03C7,

        /// <summary>Barcode.</summary>
        Barcode = 0x03C8,
    }
}
