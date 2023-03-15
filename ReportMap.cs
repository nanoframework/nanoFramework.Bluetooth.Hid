
namespace Bluetooth
{
    /// <summary>
    /// The report map service allows to map the different service to usage= and characteristics values.
    /// https://usb.org/document-library/hid-usage-tables-13
    /// </summary>
    public static class ReportMap
    {
        public static byte[] Mouse = new byte[] {
            // MOUSE REPORT
            0x05, 0x01,  // Usage Page (Generic Desktop)
            0x09, 0x02,  // Usage (Mouse)
            0xA1, 0x01,  // Collection (Application)
            0x85, 0x01,  // Report Id (1)
            0x09, 0x01,  //   Usage (Pointer)
            0xA1, 0x00,  //   Collection (Physical)
            0x05, 0x09,  //     Usage Page (Buttons)
            0x19, 0x01,  //     Usage Minimum (01) - Button 1
            0x29, 0x03,  //     Usage Maximum (03) - Button 3
            0x15, 0x00,  //     Logical Minimum (0)
            0x25, 0x01,  //     Logical Maximum (1)
            0x75, 0x01,  //     Report Size (1)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x02,  //     Input (Data, Variable, Absolute) - Button states
            0x75, 0x05,  //     Report Size (5)
            0x95, 0x01,  //     Report Count (1)
            0x81, 0x01,  //     Input (Constant) - Padding or Reserved bits
            0x05, 0x01,  //     Usage Page (Generic Desktop)
            0x09, 0x30,  //     Usage (X)
            0x09, 0x31,  //     Usage (Y)
            0x09, 0x38,  //     Usage (Wheel)
            0x15, 0x81,  //     Logical Minimum (-127)
            0x25, 0x7F,  //     Logical Maximum (127)
            0x75, 0x08,  //     Report Size (8)
            0x95, 0x03,  //     Report Count (3)
            0x81, 0x06,  //     Input (Data, Variable, Relative) - X coordinate, Y coordinate, wheel
            0xC0,        //   End Collection
            0xC0,        // End Collection
        };

        public static byte[] Keyboard = new byte[] {
            // KEYBOARD REPORT
            0x05, 0x01,  // Usage Pg (Generic Desktop)
            0x09, 0x06,  // Usage (Keyboard)
            0xA1, 0x01,  // Collection: (Application)
            0x85, 0x02,  // Report Id (2)
            0x05, 0x07,  //   Usage Pg (Key Codes)
            0x19, 0xE0,  //   Usage Min (224)
            0x29, 0xE7,  //   Usage Max (231)
            0x15, 0x00,  //   Log Min (0)
            0x25, 0x01,  //   Log Max (1)
            //   Modifier byte
            0x75, 0x01,  //   Report Size (1)
            0x95, 0x08,  //   Report Count (8)
            0x81, 0x02,  //   Input: (Data, Variable, Absolute)
            //   Reserved byte
            0x95, 0x01,  //   Report Count (1)
            0x75, 0x08,  //   Report Size (8)
            0x81, 0x01,  //   Input: (Constant)
            //   LED report
            0x95, 0x05,  //   Report Count (5)
            0x75, 0x01,  //   Report Size (1)
            0x05, 0x08,  //   Usage Pg (LEDs)
            0x19, 0x01,  //   Usage Min (1)
            0x29, 0x05,  //   Usage Max (5)
            0x91, 0x02,  //   Output: (Data, Variable, Absolute)
            //   LED report padding
            0x95, 0x01,  //   Report Count (1)
            0x75, 0x03,  //   Report Size (3)
            0x91, 0x01,  //   Output: (Constant)
            //   Key arrays (6 bytes)
            0x95, 0x06,  //   Report Count (6)
            0x75, 0x08,  //   Report Size (8)
            0x15, 0x00,  //   Log Min (0)
            0x25, 0x65,  //   Log Max (101)
            0x05, 0x07,  //   Usage Pg (Key Codes)
            0x19, 0x00,  //   Usage Min (0)
            0x29, 0x65,  //   Usage Max (101)
            0x81, 0x00,  //   Input: (Data, Array)
            0xC0,        // End Collection
        };

        public static byte[] ConsumerDevice = new byte[] {
            // CONSUMER DEVICE REPORT
            0x05, 0x0C,   // Usage Pg (Consumer Devices)
            0x09, 0x01,   // Usage (Consumer Control)
            0xA1, 0x01,   // Collection (Application)
            0x85, 0x03,   // Report Id (3)
            0x09, 0x02,   //   Usage (Numeric Key Pad)
            0xA1, 0x02,   //   Collection (Logical)
            0x05, 0x09,   //     Usage Pg (Button)
            0x19, 0x01,   //     Usage Min (Button 1)
            0x29, 0x0A,   //     Usage Max (Button 10)
            0x15, 0x01,   //     Logical Min (1)
            0x25, 0x0A,   //     Logical Max (10)
            0x75, 0x04,   //     Report Size (4)
            0x95, 0x01,   //     Report Count (1)
            0x81, 0x00,   //     Input (Data, Ary, Abs)
            0xC0,         //   End Collection
            0x05, 0x0C,   //   Usage Pg (Consumer Devices)
            0x09, 0x86,   //   Usage (Channel)
            0x15, 0xFF,   //   Logical Min (-1)
            0x25, 0x01,   //   Logical Max (1)
            0x75, 0x02,   //   Report Size (2)
            0x95, 0x01,   //   Report Count (1)
            0x81, 0x46,   //   Input (Data, Var, Rel, Null)
            0x09, 0xE9,   //   Usage (Volume Up)
            0x09, 0xEA,   //   Usage (Volume Down)
            0x15, 0x00,   //   Logical Min (0)
            0x75, 0x01,   //   Report Size (1)
            0x95, 0x02,   //   Report Count (2)
            0x81, 0x02,   //   Input (Data, Var, Abs)
            0x09, 0xE2,   //   Usage (Mute)
            0x09, 0x30,   //   Usage (Power)
            0x09, 0x83,   //   Usage (Recall Last)
            0x09, 0x81,   //   Usage (Assign Selection)
            0x09, 0xB0,   //   Usage (Play)
            0x09, 0xB1,   //   Usage (Pause)
            0x09, 0xB2,   //   Usage (Record)
            0x09, 0xB3,   //   Usage (Fast Forward)
            0x09, 0xB4,   //   Usage (Rewind)
            0x09, 0xB5,   //   Usage (Scan Next)
            0x09, 0xB6,   //   Usage (Scan Prev)
            0x09, 0xB7,   //   Usage (Stop)
            0x15, 0x01,   //   Logical Min (1)
            0x25, 0x0C,   //   Logical Max (12)
            0x75, 0x04,   //   Report Size (4)
            0x95, 0x01,   //   Report Count (1)
            0x81, 0x00,   //   Input (Data, Ary, Abs)
            0x09, 0x80,   //   Usage (Selection)
            0xA1, 0x02,   //   Collection (Logical)
            0x05, 0x09,   //     Usage Pg (Button)
            0x19, 0x01,   //     Usage Min (Button 1)
            0x29, 0x03,   //     Usage Max (Button 3)
            0x15, 0x01,   //     Logical Min (1)
            0x25, 0x03,   //     Logical Max (3)
            0x75, 0x02,   //     Report Size (2)
            0x81, 0x00,   //     Input (Data, Ary, Abs)
            0xC0,         //   End Collection
            0x81, 0x03,   //   Input (Const, Var, Abs)
            0xC0,         // End Collection
        };
    }
}
