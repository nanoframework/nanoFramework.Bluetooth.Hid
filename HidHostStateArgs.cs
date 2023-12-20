// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace nanoFramework.Bluetooth.Hid
{
    /// <summary>
    /// HID Host State Event Args.
    /// </summary>
    public class HidHostStateArgs : EventArgs
    {
        /// <summary>
        /// Gets a value indicating whether the HID host has been suspended.
        /// </summary>
        /// <remarks>
        /// This value can be used as a signal to put the HID device in a power-saving state while the host is suspended.
        /// </remarks>
        public bool IsHostSuspended { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HidHostStateArgs"/> class.
        /// </summary>
        /// <param name="hostStateFlag">HID Host State value. 0x00 = Suspend, 0x01 = Exit Suspend.</param>
        public HidHostStateArgs(byte hostStateFlag)
        {
            /*
             * Possible flags according to Bluetooth HID Service Spec
             * 0x00 = Suspend: Host is entering suspended state.
             * 0x01 = Exit Suspend: Host is exiting suspended state.
             */

            IsHostSuspended = hostStateFlag == 0x00;
        }
    }
}
