// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid.Devices
{
    /// <summary>
    /// The set of keys that are supported by a <see cref="Keyboard"/> instance.
    /// </summary>
    public static class Keys
    {
        /// <summary>
        /// The set of alphabetical keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Alphabet
        {
            /// <summary>
            /// A.
            /// </summary>
            public const byte A = 0x04;

            /// <summary>
            /// B.
            /// </summary>
            public const byte B = 0x05;

            /// <summary>
            /// C.
            /// </summary>
            public const byte C = 0x06;

            /// <summary>
            /// D.
            /// </summary>
            public const byte D = 0x07;

            /// <summary>
            /// E.
            /// </summary>
            public const byte E = 0x08;

            /// <summary>
            /// F.
            /// </summary>
            public const byte F = 0x09;

            /// <summary>
            /// G.
            /// </summary>
            public const byte G = 0x0A;

            /// <summary>
            /// H.
            /// </summary>
            public const byte H = 0x0B;

            /// <summary>
            /// I.
            /// </summary>
            public const byte I = 0x0C;

            /// <summary>
            /// J.
            /// </summary>
            public const byte J = 0x0D;

            /// <summary>
            /// K.
            /// </summary>
            public const byte K = 0x0E;

            /// <summary>
            /// L.
            /// </summary>
            public const byte L = 0x0F;

            /// <summary>
            /// M.
            /// </summary>
            public const byte M = 0x10;

            /// <summary>
            /// N.
            /// </summary>
            public const byte N = 0x11;

            /// <summary>
            /// O.
            /// </summary>
            public const byte O = 0x12;

            /// <summary>
            /// P.
            /// </summary>
            public const byte P = 0x13;

            /// <summary>
            /// Q.
            /// </summary>
            public const byte Q = 0x14;

            /// <summary>
            /// R.
            /// </summary>
            public const byte R = 0x15;

            /// <summary>
            /// S.
            /// </summary>
            public const byte S = 0x16;

            /// <summary>
            /// T.
            /// </summary>
            public const byte T = 0x17;

            /// <summary>
            /// U.
            /// </summary>
            public const byte U = 0x18;

            /// <summary>
            /// V.
            /// </summary>
            public const byte V = 0x19;

            /// <summary>
            /// W.
            /// </summary>
            public const byte W = 0x1A;

            /// <summary>
            /// X.
            /// </summary>
            public const byte X = 0x1B;

            /// <summary>
            /// Y.
            /// </summary>
            public const byte Y = 0x1C;

            /// <summary>
            /// Z.
            /// </summary>
            public const byte Z = 0x1D;
        }

        /// <summary>
        /// The set of numerical keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Numeric
        {
            /// <summary>
            /// Number Keys Row 1.
            /// </summary>
            public const byte Keyboard1 = 0x1E;

            /// <summary>
            /// Number Keys Row 2.
            /// </summary>
            public const byte Keyboard2 = 0x1F;

            /// <summary>
            /// Number Keys Row 3.
            /// </summary>
            public const byte Keyboard3 = 0x20;

            /// <summary>
            /// Number Keys Row 4.
            /// </summary>
            public const byte Keyboard4 = 0x21;

            /// <summary>
            /// Number Keys Row 5.
            /// </summary>
            public const byte Keyboard5 = 0x22;

            /// <summary>
            /// Number Keys Row 6.
            /// </summary>
            public const byte Keyboard6 = 0x23;

            /// <summary>
            /// Number Keys Row 7.
            /// </summary>
            public const byte Keyboard7 = 0x24;

            /// <summary>
            /// Number Keys Row 8.
            /// </summary>
            public const byte Keyboard8 = 0x25;

            /// <summary>
            /// Number Keys Row 9.
            /// </summary>
            public const byte Keyboard9 = 0x26;

            /// <summary>
            /// Number Keys Row 0.
            /// </summary>
            public const byte Keyboard0 = 0x27;
        }

        /// <summary>
        /// The set of control keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Control
        {
            /// <summary>
            /// Return (Enter).
            /// </summary>
            public const byte Return = 0x28;

            /// <summary>
            /// Escape.
            /// </summary>
            public const byte Escape = 0x29;

            /// <summary>
            /// Backspace.
            /// </summary>
            public const byte Backspace = 0x2A;

            /// <summary>
            /// Tab.
            /// </summary>
            public const byte Tab = 0x2B;

            /// <summary>
            /// Space.
            /// </summary>
            public const byte Space = 0x2C;
        }

        /// <summary>
        /// The set of symbol keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Symbols
        {
            /// <summary>
            /// Minus (-).
            /// </summary>
            public const byte Minus = 0x2D;

            /// <summary>
            /// Equal (=).
            /// </summary>
            public const byte Equal = 0x2E;

            /// <summary>
            /// Open Square Bracket ([).
            /// </summary>
            public const byte OpenSquareBracket = 0x2F;

            /// <summary>
            /// Close Square Bracket (]).
            /// </summary>
            public const byte CloseSquareBracket = 0x30;

            /// <summary>
            /// Backslash (\).
            /// </summary>
            public const byte Backslash = 0x31;

            /// <summary>
            /// Pound (#).
            /// </summary>
            public const byte Pound = 0x32;

            /// <summary>
            /// Semicolon (;).
            /// </summary>
            public const byte Semicolon = 0x33;

            /// <summary>
            /// Quote (").
            /// </summary>
            public const byte Quote = 0x34;

            /// <summary>
            /// Grave Accent (`).
            /// </summary>
            public const byte GraveAccent = 0x35;

            /// <summary>
            /// Comma (,).
            /// </summary>
            public const byte Comma = 0x36;

            /// <summary>
            /// Dot (.).
            /// </summary>
            public const byte Dot = 0x37;

            /// <summary>
            /// Forward Slash (/).
            /// </summary>
            public const byte Forwardslash = 0x38;
        }

        /// <summary>
        /// The set of function keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Functions
        {
            /// <summary>
            /// Function Key F1.
            /// </summary>
            public const byte F1 = 0x3A;

            /// <summary>
            /// Function Key F2.
            /// </summary>
            public const byte F2 = 0x3B;

            /// <summary>
            /// Function Key F3.
            /// </summary>
            public const byte F3 = 0x3C;

            /// <summary>
            /// Function Key F4.
            /// </summary>
            public const byte F4 = 0x3D;

            /// <summary>
            /// Function Key F5.
            /// </summary>
            public const byte F5 = 0x3E;

            /// <summary>
            /// Function Key F6.
            /// </summary>
            public const byte F6 = 0x3F;

            /// <summary>
            /// Function Key F7.
            /// </summary>
            public const byte F7 = 0x40;

            /// <summary>
            /// Function Key F8.
            /// </summary>
            public const byte F8 = 0x41;

            /// <summary>
            /// Function Key F9.
            /// </summary>
            public const byte F9 = 0x42;

            /// <summary>
            /// Function Key F10.
            /// </summary>
            public const byte F10 = 0x43;

            /// <summary>
            /// Function Key F11.
            /// </summary>
            public const byte F12 = 0x44;

            /// <summary>
            /// Function Key F12.
            /// </summary>
            public const byte F13 = 0x45;
        }

        /// <summary>
        /// The set of special keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Specials
        {
            /// <summary>
            /// Caps Lock.
            /// </summary>
            public const byte CapsLock = 0x39;

            /// <summary>
            /// Print Screen.
            /// </summary>
            public const byte PrintScr = 0x46;

            /// <summary>
            /// Scroll Lock.
            /// </summary>
            public const byte ScrlLock = 0x47;

            /// <summary>
            /// Pause.
            /// </summary>
            public const byte Pause = 0x48;

            /// <summary>
            /// Insert.
            /// </summary>
            public const byte Insert = 0x49;

            /// <summary>
            /// Home.
            /// </summary>
            public const byte Home = 0x4A;

            /// <summary>
            /// Page Up.
            /// </summary>
            public const byte PageUp = 0x4B;

            /// <summary>
            /// Delete Forward.
            /// </summary>
            public const byte DeleteFwd = 0x4C;

            /// <summary>
            /// End.
            /// </summary>
            public const byte End = 0x4D;

            /// <summary>
            /// Page Down.
            /// </summary>
            public const byte PageDown = 0x4E;

            /// <summary>
            /// Right Arrow.
            /// </summary>
            public const byte RightArrow = 0x4F;

            /// <summary>
            /// Left Arrow.
            /// </summary>
            public const byte LeftArrow = 0x50;

            /// <summary>
            /// Down Arrow.
            /// </summary>
            public const byte DownArrow = 0x51;

            /// <summary>
            /// Up Arrow.
            /// </summary>
            public const byte UpArrow = 0x52;
        }

        /// <summary>
        /// Set of keypad keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Keypad
        {
            /// <summary>
            /// Keypad Num Lock.
            /// </summary>
            public const byte KeypadNumLck = 0x53;

            /// <summary>
            /// Keypad Forward Slash.
            /// </summary>
            public const byte KeypadForwardslash = 0x54;

            /// <summary>
            /// Keypad Asterisk.
            /// </summary>
            public const byte KeypadAsterisk = 0x55;

            /// <summary>
            /// Keypad Minus.
            /// </summary>
            public const byte KeypadMinus = 0x56;

            /// <summary>
            /// Keypad Plus.
            /// </summary>
            public const byte KeypadPlus = 0x57;

            /// <summary>
            /// Keypad Enter.
            /// </summary>
            public const byte KeypadEnter = 0x58;

            /// <summary>
            /// Keypad Key 1.
            /// </summary>
            public const byte Keypad1 = 0x59;

            /// <summary>
            /// Keypad Key 2.
            /// </summary>
            public const byte Keypad2 = 0x5A;

            /// <summary>
            /// Keypad Key 3.
            /// </summary>
            public const byte Keypad3 = 0x5B;

            /// <summary>
            /// Keypad Key 4.
            /// </summary>
            public const byte Keypad4 = 0x5C;

            /// <summary>
            /// Keypad Key 5.
            /// </summary>
            public const byte Keypad5 = 0x5D;

            /// <summary>
            /// Keypad Key 6.
            /// </summary>
            public const byte Keypad6 = 0x5E;

            /// <summary>
            /// Keypad Key 7.
            /// </summary>
            public const byte Keypad7 = 0x5F;

            /// <summary>
            /// Keypad Key 8.
            /// </summary>
            public const byte Keypad8 = 0x60;

            /// <summary>
            /// Keypad Key 9.
            /// </summary>
            public const byte Keypad9 = 0x61;

            /// <summary>
            /// Keypad Key 10.
            /// </summary>
            public const byte Keypad0 = 0x62;

            /// <summary>
            /// Dot (.).
            /// </summary>
            public const byte KeypadDot = 0x63;
        }

        /// <summary>
        /// Set of modifier keys supported by a <see cref="Keyboard"/> instance.
        /// </summary>
        public static class Modifiers
        {
            /// <summary>
            /// Left Control Key.
            /// </summary>
            public const byte LeftCtrl = 0xE0;

            /// <summary>
            /// Left Shift Key.
            /// </summary>
            public const byte LeftShift = 0xE1;

            /// <summary>
            /// Left Alt Key.
            /// </summary>
            public const byte LeftAlt = 0xE2;

            /// <summary>
            /// Left GUI Key (Windows Logo, MAC Command).
            /// </summary>
            public const byte LeftGUI = 0xE3;

            /// <summary>
            /// Right Control Key.
            /// </summary>
            public const byte RightCtrl = 0xE4;

            /// <summary>
            /// Right Shift Key.
            /// </summary>
            public const byte RightShift = 0xE5;

            /// <summary>
            /// Right Alt Key.
            /// </summary>
            public const byte RightAlt = 0xE6;

            /// <summary>
            /// Right GUI Key (Windows Logo, MAC Command).
            /// </summary>
            public const byte RightGUI = 0xE7;
        }

        /// <summary>
        /// Helper method to check if the specified key is a modifier key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True, if the key is a modifier key.</returns>
        public static bool IsModifierKey(byte key)
        {
            return key >= Modifiers.LeftCtrl
                && key <= Modifiers.RightGUI;
        }

        /// <summary>
        /// Helper method to check if the specified key is a letter key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True, if the key is a letter key.</returns>
        public static bool IsLetter(byte key)
        {
            return key >= Alphabet.A
                && key <= Alphabet.Z;
        }

        /// <summary>
        /// Helper method to check if the specified key is a digit key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true, if the key is a digit key.</returns>
        public static bool IsDigit(byte key)
        {
            return (key >= Numeric.Keyboard1
                && key <= Numeric.Keyboard0)
                || (key >= Keypad.Keypad1
                && key <= Keypad.Keypad0);
        }
    }
}
