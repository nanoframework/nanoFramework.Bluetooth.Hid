namespace nanoFramework.Bluetooth.HID
{
    public class Keyboard : Device
    {
        protected override byte[] GetReportMap()
        {
            return new byte[] {
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
        }
    }
}