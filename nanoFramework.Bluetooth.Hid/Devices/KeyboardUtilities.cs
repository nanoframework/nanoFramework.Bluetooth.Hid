// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Diagnostics;

using static nanoFramework.Bluetooth.Hid.Devices.Keys;

namespace nanoFramework.Bluetooth.Hid.Devices
{
    /// <summary>
    /// A class containing helper methods to use with <see cref="Keyboard"/>.
    /// </summary>
    public static class KeyboardUtilities
    {
        private static readonly Hashtable KeyMap = new Hashtable()
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

        /// <summary>
        /// Simulate typing a string on a keyboard.
        /// </summary>
        /// <param name="keyboard">The keyboard to simulate the typing on.</param>
        /// <param name="input">The string input to type on the keyboard.</param>
        /// <remarks>
        /// This method only supports ASCII characters found on English keyboards.
        /// </remarks>
        public static void TypeText(Keyboard keyboard, string input)
        {
            keyboard.ReleaseAll();

            foreach (char character in input)
            {
                try
                {
                    var upperCaseCharacter = character.ToUpper();
                    var key = KeyMap[upperCaseCharacter];

                    // if the character is not in the map, move to the next one
                    if (key == null)
                    {
                        continue;
                    }

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
                catch
                {
                }
            }
        }
    }
}
