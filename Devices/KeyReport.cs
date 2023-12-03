using System;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public class KeyReport
	{
		public byte Modifiers { get; set; }

		public byte ReservedByte { get; set; }

		public byte[] Keys { get; }

		public KeyReport(byte maxNumPressedKey)
		{
			Keys = new byte[maxNumPressedKey];
		}

		public void AddKey(byte key)
		{
			if (KeyMap.IsModifierKey(key))
			{
				var modifierKeyMask = 0x80 >> (7 - (key - KeyMap.LeftCtrl));
				Modifiers |= (byte)modifierKeyMask;

				return;
			}

			var firstAvailableSlot = -1;
			for (var slotIndex = 0; slotIndex < Keys.Length; slotIndex++)
			{
				var slotKey = Keys[slotIndex];

				// only 'press' the key if it has not been pressed before
				if (slotKey == key)
				{
					return;
				}

				if (firstAvailableSlot == -1
					&& slotKey == 0x00)
				{
					firstAvailableSlot = slotIndex;
				}
			}

			if (firstAvailableSlot > -1)
			{
				Keys[firstAvailableSlot] = key;
			}
			else
			{
				// throw if the key cannot go in the report
				throw new InvalidOperationException();
			}
		}

		public void RemoveKey(byte key)
		{
			for (var slotIndex = 0; slotIndex < Keys.Length; slotIndex++)
			{
				if (Keys[slotIndex] == key)
				{
					Keys[slotIndex] = 0x00;
				}
			}
		}

		public void Reset()
		{
			Modifiers = 0;
			ReservedByte = 0;

			for (var i = 0; i < Keys.Length; i++)
			{
				Keys[i] = 0x00;
			}
		}

		public byte[] ToBytes()
		{
			var result = new byte[2 + Keys.Length];

			result[0] = Modifiers;
			result[1] = ReservedByte;

			Array.Copy(
				sourceArray: Keys,
				sourceIndex: 0,
				destinationArray: result,
				destinationIndex: 2,
				length: Keys.Length);

			return result;
		}
	}
}
