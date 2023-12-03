using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace nanoFramework.Bluetooth.HID.Devices
{
	public static class Keys
	{
		private static readonly Hashtable KeyMap = new()
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
		public const byte Tilde 

		public static byte[] GetKeys(string input)
		{
			var keysArr = new byte[input.Length];

			for (var charIndex = 0; charIndex < input.Length; charIndex++)
			{
				var character = input[charIndex];
				if (KeyMap[character.ToUpper()] is byte value)
				{
					keysArr[charIndex] = (byte)value;
				}
			}

			return keysArr;
		}
	}
}
