// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid.Devices
{
    /// <summary>
    /// A class representing the LED statuses on a <see cref="Keyboard"/>.
    /// </summary>
    public class LedStatus
    {
        /// <summary>
        /// Gets a value indicating whether the num lock key LED is on.
        /// </summary>
        public bool IsNumLockOn { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the caps lock key LED is on.
        /// </summary>
        public bool IsCapsLockOn { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the scroll lock key LED is on.
        /// </summary>
        public bool IsSrollLockOn { get; internal set; }
    }
}
