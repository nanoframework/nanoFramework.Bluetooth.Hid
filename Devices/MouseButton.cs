// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.HID.Devices
{
    /// <summary>
    /// Supported mouse buttons.
    /// </summary>
    public enum MouseButton : byte
    {
        /// <summary>Mouse Left.</summary>
        Left = 0x01,

        /// <summary>Mouse Right.</summary> 
        Right = 0x02,

        /// <summary>Mouse Middle.</summary> 
        Middle = 0x03,
    }
}
