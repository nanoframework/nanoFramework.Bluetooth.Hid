// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Device.Bluetooth;

namespace nanoFramework.Bluetooth.HID.Extensions
{
    internal static class BufferExtensions
    {
        internal static Buffer AsBuffer(this byte byteValue)
        {
            return (new[] { byteValue }).AsBuffer();
        }

        internal static Buffer AsBuffer(this byte[] byteArray)
        {
            return new(byteArray);
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
