using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;

namespace nanoFramework.Bluetooth.HID
{
    public sealed class PnpElements
    {
        public byte Sig { get; }

        public ushort Vid { get; }

        public ushort Pid { get; }

        public ushort Version { get; }

        public PnpElements(byte sig, ushort vid, ushort pid, ushort version)
        {
            Sig = sig;
            Vid = vid;
            Pid = pid;
            Version = version;
        }

        public byte[] Serialize()
        {
            return new byte[]
            {
                Sig,
                (byte)(Vid >> 8),
                (byte)Vid,
                (byte)(Pid >> 8),
                (byte)Pid,
                (byte)(Version >> 8),
                (byte)Version
            };
        }

        public Buffer ToBuffer()
        {
            return this.Serialize()
                .AsBuffer();
        }
    }
}
