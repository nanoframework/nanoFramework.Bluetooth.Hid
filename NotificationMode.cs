// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid
{
    /// <summary>
    /// The notification mode supported by client configuration characteristic.
    /// </summary>
    public enum NotificationMode : ushort
    {
        /// <summary>
        /// Notifications and Indication disabled.
        /// </summary>
        Disabled = 0x0000,

        /// <summary>
        /// Enable notifications.
        /// </summary>
        EnableNotifications = 0x0001,

        /// <summary>
        /// Enable indication.
        /// </summary>
        EnableIndication = 0x0002
    }
}
