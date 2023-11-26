using System;

namespace nanoFramework.Bluetooth.HID
{
    public sealed class HidHostStateArgs : EventArgs
    {
        public bool IsHostSuspended { get; }

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
