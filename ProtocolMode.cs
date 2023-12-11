// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.HID
{
    /// <summary>
    /// The protocol to be used in the HID Service.
    /// </summary>
    public enum ProtocolMode : byte
    {
        /// <summary>
        /// Boot mode. Used for compatibility with older hosts.
        /// Only works for keyboard and mouse devices.
        /// </summary>
        Boot = 0x00,

        /// <summary>
        /// Report mode. HID Device will send and receive reports according
        /// to its Report Map.
        /// </summary>
        Report = 0x01,
    }
}
