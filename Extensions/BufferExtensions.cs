using nanoFramework.Device.Bluetooth;

namespace nanoFramework.Bluetooth.HID.Extensions
{
    internal static class BufferExtensions
    {
        internal static Buffer ToBuffer(this byte byteValue)
        {
            var dw = new DataWriter();
            dw.WriteByte(byteValue);
            return dw.DetachBuffer();
        }

        internal static Buffer ToBuffer(this byte[] byteArray)
        {
            var dw = new DataWriter();
            dw.WriteBytes(byteArray);
            return dw.DetachBuffer();
        }

        internal static Buffer ToBuffer(this string str)
        {
            var dw = new DataWriter();
            dw.WriteString(str);
            return dw.DetachBuffer();
        }

        internal static Buffer ToBuffer(this ushort val)
        {
            var dw = new DataWriter();
            dw.WriteUInt16(val);
            return dw.DetachBuffer();
        }

        internal static byte AsByte(this Buffer buffer)
        {
            var dataReader = DataReader.FromBuffer(buffer);
            return dataReader.ReadByte();
        }
    }
}
