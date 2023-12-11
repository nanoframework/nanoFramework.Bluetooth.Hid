// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.HID.Devices
{
    public sealed class LedStatus
    {
        public bool IsNumLockOn { get; internal set; }

        public bool IsCapsLockOn { get; internal set; }

        public bool IsSrollLockOn { get; internal set; }
    }
}
