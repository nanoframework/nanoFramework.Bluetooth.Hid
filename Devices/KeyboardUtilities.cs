﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using static nanoFramework.Bluetooth.HID.Devices.Keys;
using System.Collections;

namespace nanoFramework.Bluetooth.HID.Devices
{
    public static class KeyboardUtilities
    {
        private static readonly Hashtable map = new()
        {
            { 'A',  Alphabet.A },
            { 'B',  Alphabet.B },
            { 'C',  Alphabet.C },
            { 'D',  Alphabet.D },
            { 'E',  Alphabet.E },
            { 'F',  Alphabet.F },
            { 'G',  Alphabet.G },
            { 'H',  Alphabet.H },
            { 'I',  Alphabet.I },
            { 'J',  Alphabet.J },
            { 'K',  Alphabet.K },
            { 'L',  Alphabet.L },
            { 'M',  Alphabet.M },
            { 'N',  Alphabet.N },
            { 'O',  Alphabet.O },
            { 'P',  Alphabet.P },
            { 'Q',  Alphabet.Q },
            { 'R',  Alphabet.R },
            { 'S',  Alphabet.S },
            { 'T',  Alphabet.T },
            { 'U',  Alphabet.U },
            { 'V',  Alphabet.V },
            { 'W',  Alphabet.W },
            { 'X',  Alphabet.X },
            { 'Y',  Alphabet.Y },
            { 'Z',  Alphabet.Z },

            { '1',  Numeric.Keyboard1 },
            { '2',  Numeric.Keyboard2 },
            { '3',  Numeric.Keyboard3 },
            { '4',  Numeric.Keyboard4 },
            { '5',  Numeric.Keyboard5 },
            { '6',  Numeric.Keyboard6 },
            { '7',  Numeric.Keyboard7 },
            { '8',  Numeric.Keyboard8 },
            { '9',  Numeric.Keyboard9 },
            { '0',  Numeric.Keyboard0 },

            { ' ',  Control.Space },

            { '-',  Symbols.Minus },
            { '=',  Symbols.Equal },
            { '[',  Symbols.OpenSquareBracket },
            { ']',  Symbols.CloseSquareBracket },
            { '\\', Symbols.Backslash },
            { ';',  Symbols.Semicolon },
            { '\'', Symbols.Quote },
            { ',',  Symbols.Comma },
            { '`',  Symbols.GraveAccent },
            { '.',  Symbols.Dot },
            { '/',  Symbols.Forwardslash }
        };

        public static void TypeText(Keyboard keyboard, string input)
        {
            keyboard.ReleaseAll();

            foreach (char character in input)
            {
                var upperCaseCharacter = character.ToUpper();
                var key = map[upperCaseCharacter];
                var keyVal = (byte)key;

                if (key != null)
                {
                    if (IsLetter(keyVal) && upperCaseCharacter == character)
                    {
                        keyboard.Press(Modifiers.LeftShift);
                    }

                    keyboard.Press((byte)key);
                }

                keyboard.ReleaseAll();
            }
        }
    }
}
