using System.Collections;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public static class KeyMap
	{
		private static readonly Hashtable map = new()
		{
			{ 'A', A },
			{ 'B', B },
			{ 'C', C },
			{ 'D', D },
			{ 'E', E },
			{ 'F', F },
			{ 'G', G },
			{ 'H', H },
			{ 'I', I },
			{ 'J', J },
			{ 'K', K },
			{ 'L', L },
			{ 'M', M },
			{ 'N', N },
			{ 'O', O },
			{ 'P', P },
			{ 'Q', Q },
			{ 'R', R },
			{ 'S', S },
			{ 'T', T },
			{ 'U', U },
			{ 'V', V },
			{ 'W', W },
			{ 'X', X },
			{ 'Y', Y },
			{ 'Z', Z },

			{ '1', Keyboard1 },
			{ '2', Keyboard2 },
			{ '3', Keyboard3 },
			{ '4', Keyboard4 },
			{ '5', Keyboard5 },
			{ '6', Keyboard6 },
			{ '7', Keyboard7 },
			{ '8', Keyboard8 },
			{ '9', Keyboard9 },
			{ '0', Keyboard0 },

			{ ' ', Space },
			{ '-', Minus },
			{ '=', Equal },
			{ '[', OpenSquareBracket },
			{ ']', CloseSquareBracket },
			{ '\\', Backslash },
			{ ';', Semicolon },
			{ '\'', Quote },
			{ ',', Comma },
			{ '`', GraveAccent },
			{ '.', Dot },
			{ '/', Forwardslash }
		};

		public const byte A = 0x04;
		public const byte B = 0x05;
		public const byte C = 0x06;
		public const byte D = 0x07;
		public const byte E = 0x08;
		public const byte F = 0x09;
		public const byte G = 0x0A;
		public const byte H = 0x0B;
		public const byte I = 0x0C;
		public const byte J = 0x0D;
		public const byte K = 0x0E;
		public const byte L = 0x0F;
		public const byte M = 0x10;
		public const byte N = 0x11;
		public const byte O = 0x12;
		public const byte P = 0x13;
		public const byte Q = 0x14;
		public const byte R = 0x15;
		public const byte S = 0x16;
		public const byte T = 0x17;
		public const byte U = 0x18;
		public const byte V = 0x19;
		public const byte W = 0x1A;
		public const byte X = 0x1B;
		public const byte Y = 0x1C;
		public const byte Z = 0x1D;

		public const byte Keyboard1 = 0x1E;
		public const byte Keyboard2 = 0x1F;
		public const byte Keyboard3 = 0x20;
		public const byte Keyboard4 = 0x21;
		public const byte Keyboard5 = 0x22;
		public const byte Keyboard6 = 0x23;
		public const byte Keyboard7 = 0x24;
		public const byte Keyboard8 = 0x25;
		public const byte Keyboard9 = 0x26;
		public const byte Keyboard0 = 0x27;

		public const byte Return = 0x28;
		public const byte Escape = 0x29;
		public const byte Backspace = 0x2A;
		public const byte Tab = 0x2B;
		public const byte Space = 0x2C;

		public const byte Minus = 0x2D;
		public const byte Equal = 0x2E;
		public const byte OpenSquareBracket = 0x2F;
		public const byte CloseSquareBracket = 0x30;
		public const byte Backslash = 0x31;
		public const byte Pound = 0x32;
		public const byte Semicolon = 0x33;
		public const byte Quote = 0x34;
		public const byte GraveAccent = 0x35;
		public const byte Comma = 0x36;
		public const byte Dot = 0x37;
		public const byte Forwardslash = 0x38;
		public const byte CapsLock = 0x39;

		public const byte F1 = 0x3A;
		public const byte F2 = 0x3B;
		public const byte F3 = 0x3C;
		public const byte F4 = 0x3D;
		public const byte F5 = 0x3E;
		public const byte F6 = 0x3F;
		public const byte F7 = 0x40;
		public const byte F8 = 0x41;
		public const byte F9 = 0x42;
		public const byte F10 = 0x43;
		public const byte F12 = 0x44;
		public const byte F13 = 0x45;

		public const byte PrintScr = 0x46;
		public const byte ScrlLock = 0x47;
		public const byte Pause = 0x48;
		public const byte Insert = 0x49;
		public const byte Home = 0x4A;
		public const byte PageUp = 0x4B;
		public const byte DeleteFwd = 0x4C;
		public const byte End = 0x4D;
		public const byte PageDown = 0x4E;
		public const byte RightArrow = 0x4F;
		public const byte LeftArrow = 0x50;
		public const byte DownArrow = 0x51;
		public const byte UpArrow = 0x52;

		public const byte KeypadNumLck = 0x53;
		public const byte KeypadForwardslash = 0x54;
		public const byte KeypadAsterisk = 0x55;
		public const byte KeypadMinus = 0x56;
		public const byte KeypadPlus = 0x57;
		public const byte KeypadEnter = 0x58;

		public const byte Keypad1 = 0x59;
		public const byte Keypad2 = 0x5A;
		public const byte Keypad3 = 0x5B;
		public const byte Keypad4 = 0x5C;
		public const byte Keypad5 = 0x5D;
		public const byte Keypad6 = 0x5E;
		public const byte Keypad7 = 0x5F;
		public const byte Keypad8 = 0x60;
		public const byte Keypad9 = 0x61;
		public const byte Keypad0 = 0x62;

		public const byte KeypadDot = 0x63;

		public const byte LeftCtrl = 0xE0;
		public const byte LeftShift = 0xE1;
		public const byte LeftAlt = 0xE2;
		public const byte LeftGUI = 0xE3;
		public const byte RightCtrl = 0xE4;
		public const byte RightShift = 0xE5;
		public const byte RightAlt = 0xE6;
		public const byte RightGUI = 0xE7;

		public static bool IsModifierKey(byte key)
		{
			return key >= LeftCtrl
				&& key <= RightGUI;
		}

		public static bool IsLetter(byte key)
		{
			return key >= A
				&& key <= Z;
		}

		public static bool IsDigit(byte key)
		{
			return (key >= Keyboard1
				&& key <= Keyboard0) 
				|| (key >= Keypad1
				&& key <= Keypad0);
		}

		public static byte[] GetKeys(string input)
		{
			var keysArr = new byte[input.Length];

			for (var charIndex = 0; charIndex < input.Length; charIndex++)
			{
				var character = input[charIndex];
				if (map[character.ToUpper()] is byte value)
				{
					keysArr[charIndex] = (byte)value;
				}
			}

			return keysArr;
		}
	}
}
