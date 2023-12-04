using System;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public sealed class LedStatus
	{
		public bool IsNumLockOn { get; internal set; }

		public bool IsCapsLockOn { get; internal set; }

		public bool IsSrollLockOn { get; internal set; }
	}
}
