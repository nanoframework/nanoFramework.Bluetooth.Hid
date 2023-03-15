

namespace Bluetooth.Enums
{
    /// <summary>
    /// Main keyboard HID usage ID. More are available, those are the most used on a traditional keyboard or keypad.
    /// https://usb.org/document-library/hid-usage-tables-13
    /// </summary>
    public enum HidKeyboardKey
    {
        /// <summary>No event inidicated.</summary>
        KeyReserved = 0,

        /// <summary>Keyboard a and A.</summary>
        KeyA = 4,

        /// <summary>Keyboard b and B.</summary>
        KeyB = 5,

        /// <summary>Keyboard c and C.</summary>
        KeyC = 6,

        /// <summary>Keyboard d and D.</summary>
        KeyD = 7,

        /// <summary>Keyboard e and E.</summary>
        KeyE = 8,

        /// <summary>Keyboard f and F.</summary>
        KeyF = 9,

        /// <summary>Keyboard g and G.</summary>
        KeyG = 10,

        /// <summary>Keyboard h and H.</summary>
        KeyH = 11,

        /// <summary>Keyboard i and I.</summary>
        KeyI = 12,

        /// <summary>Keyboard j and J.</summary>
        KeyJ = 13,

        /// <summary>Keyboard k and K.</summary>
        KeyK = 14,

        /// <summary>Keyboard l and L.</summary>
        KeyL = 15,

        /// <summary>Keyboard m and M.</summary>
        KeyM = 16,

        /// <summary>Keyboard n and N.</summary>
        KeyN = 17,

        /// <summary>Keyboard o and O.</summary>
        KeyO = 18,

        /// <summary>Keyboard p and p.</summary>
        KeyP = 19,

        /// <summary>Keyboard q and Q.</summary>
        KeyQ = 20,

        /// <summary>Keyboard r and R.</summary>
        KeyR = 21,

        /// <summary>Keyboard s and S.</summary>
        KeyS = 22,

        /// <summary>Keyboard t and T.</summary>
        KeyT = 23,

        /// <summary>Keyboard u and U.</summary>
        KeyU = 24,

        /// <summary>Keyboard v and V.</summary>
        KeyV = 25,

        /// <summary>Keyboard w and W.</summary>
        KeyW = 26,

        /// <summary>Keyboard x and X.</summary>
        KeyX = 27,

        /// <summary>Keyboard y and Y.</summary>
        KeyY = 28,

        /// <summary>Keyboard z and Z.</summary>
        KeyZ = 29,

        /// <summary>Keyboard 1 and !.</summary>
        Key1 = 30,

        /// <summary>Keyboard 2 and @.</summary>
        Key2 = 31,

        /// <summary>Keyboard 3 and #.</summary>
        Key3 = 32,

        /// <summary>Keyboard 4 and %.</summary>
        Key4 = 33,

        /// <summary>Keyboard 5 and %.</summary>
        Key5 = 34,

        /// <summary>Keyboard 6 and ^.</summary>
        Key6 = 35,

        /// <summary>Keyboard 7 and &.</summary>
        Key7 = 36,

        /// <summary>Keyboard 8 and *.</summary>
        Key8 = 37,

        /// <summary>Keyboard 9 and (.</summary>
        Key9 = 38,

        /// <summary>Keyboard 0 and ).</summary>
        Key0 = 39,

        /// <summary>Keyboard Return (ENTER).</summary>
        KeyReturn = 40,

        /// <summary>Keyboard ESCAPE.</summary>
        KeyEscape = 41,

        /// <summary>Keyboard DELETE (Backspace).</summary>
        KeyDelete = 42,

        /// <summary>Keyboard Tab.</summary>
        KeyTab = 43,

        /// <summary>Keyboard Spacebar.</summary>
        KeySpacebar = 44,

        /// <summary>Keyboard - and (underscore).</summary>
        KeyMinus = 45,

        /// <summary>Keyboard = and +.</summary>
        KeyEqual = 46,

        /// <summary>Keyboard [ and {.</summary>
        KeyLeftBracket = 47,

        /// <summary>Keyboard ] and }.</summary>
        KeyRightBracket = 48,

        /// <summary>Keyboard \ and |.</summary>
        KeyBackSlash = 49,

        /// <summary>Keyboard ; and :.</summary>
        KeySemiColon = 51,

        /// <summary>Keyboard ' and ".</summary>
        KeySingleQuote = 52,

        /// <summary>Keyboard Grave Accent and Tilde.</summary>
        KeyGraveAccent = 53,

        /// <summary>Keyboard , and <.</summary>
        KeyComma = 54,

        /// <summary>Keyboard . and >.</summary>
        KeyDot = 55,

        /// <summary>Keyboard / and ?.</summary>
        KeyForwardSlash = 56,

        /// <summary>Keyboard Caps Lock.</summary>
        KeyCapsLock = 57,

        /// <summary>Keyboard F1.</summary>
        KeyF1 = 58,

        /// <summary>Keyboard F2.</summary>
        KeyF2 = 59,

        /// <summary>Keyboard F3.</summary>
        KeyF3 = 60,

        /// <summary>Keyboard F4.</summary>
        KeyF4 = 61,

        /// <summary>Keyboard F5.</summary>
        KeyF5 = 62,

        /// <summary>Keyboard F6.</summary>
        KeyF6 = 63,

        /// <summary>Keyboard F7.</summary>
        KeyF7 = 64,

        /// <summary>Keyboard F8.</summary>
        KeyF8 = 65,

        /// <summary>Keyboard F9.</summary>
        KeyF9 = 66,

        /// <summary>Keyboard F10.</summary>
        KeyF10 = 67,

        /// <summary>Keyboard F11.</summary>
        KeyF11 = 68,

        /// <summary>Keyboard F12.</summary>
        KeyF12 = 69,

        /// <summary>Keyboard Print Screen.</summary>
        KeyPrintScreen = 70,

        /// <summary>Keyboard Scroll Lock.</summary>
        KeyScrollLock = 71,

        /// <summary>Keyboard Pause.</summary>
        KeyPause = 72,

        /// <summary>Keyboard Insert.</summary>
        KeyInsert = 73,

        /// <summary>Keyboard Home.</summary>
        KeyHome = 74,

        /// <summary>Keyboard PageUp.</summary>
        KeyPageUp = 75,

        /// <summary>Keyboard Delete Forward.</summary>
        KeyDeleteForward = 76,

        /// <summary>Keyboard End.</summary>
        KeyEnd = 77,

        /// <summary>Keyboard PageDown.</summary>
        KeyPageDown = 78,

        /// <summary>Keyboard RightArrow.</summary>
        KeyRightArrow = 79,

        /// <summary>Keyboard LeftArrow.</summary>
        KeyLeftArrow = 80,

        /// <summary>Keyboard DownArrow.</summary>
        KeyDownArrow = 81,

        /// <summary>Keyboard UpArrow.</summary>
        KeyUpArrow = 82,

        /// <summary>Keypad Num Lock and Clear.</summary>
        KeyNumLock = 83,

        /// <summary>Keypad /.</summary>
        KeyDivide = 84,

        /// <summary>Keypad *.</summary>
        KeyMultiply = 85,

        /// <summary>Keypad -.</summary>
        KeySubstract = 86,

        /// <summary>Keypad +.</summary>
        KeyAdd = 87,

        /// <summary>Keypad ENTER.</summary>
        KeyEnter = 88,

        /// <summary>Keypad 1 and End.</summary>
        Keypad1 = 89,

        /// <summary>Keypad 2 and Down Arrow.</summary>
        Keypad2 = 90,

        /// <summary>Keypad 3 and PageDn.</summary>
        Keypad3 = 91,

        /// <summary>Keypad 4 and Lfet Arrow.</summary>
        Keypad4 = 92,

        /// <summary>Keypad 5.</summary>
        Keypad5 = 93,

        /// <summary>Keypad 6 and Right Arrow.</summary>
        Keypad6 = 94,

        /// <summary>Keypad 7 and Home.</summary>
        Keypad7 = 95,

        /// <summary>Keypad 8 and Up Arrow.</summary>
        Keypad8 = 96,

        /// <summary>Keypad 9 and PageUp.</summary>
        Keypad9 = 97,

        /// <summary>Keypad 0 and Insert.</summary>
        Keypad0 = 98,

        /// <summary>Keypad . and Delete.</summary>
        KeypadDot = 99,

        /// <summary>Keyboard Mute.</summary>
        KeyMute = 127,

        /// <summary>Keyboard Volume up.</summary>
        KeyVolumeUp = 128,

        /// <summary>Keyboard Volume down.</summary>
        KeyVolumeDown = 129,

        /// <summary>Keyboard LeftContorl.</summary>
        KeyLeftControl = 224,

        /// <summary>Keyboard LeftShift.</summary>
        KeyLeftShift = 225,

        /// <summary>Keyboard LeftAlt.</summary>
        KeyLeftAlt = 226,

        /// <summary>Keyboard LeftGUI.</summary>
        KeyLeftGui = 227,

        /// <summary>Keyboard LeftContorl.</summary>
        KeyRightControl = 228,

        /// <summary>Keyboard LeftShift.</summary>
        KeyRightShift = 229,

        /// <summary>Keyboard LeftAlt.</summary>
        KeyRightAlt = 230,

        /// <summary>Keyboard RightGUI.</summary>
        KeyRightGui = 231,
    }
}
