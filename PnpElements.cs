using System;

namespace Bluetooth
{
    public class PnpElements
    {
        public byte Sig { get; set; }
        public ushort Vid { get; set; }
        public ushort Pid { get; set; }
        public ushort Version { get; set; }

        public byte[] Serialize()
        {
            return new byte[] { Sig, (byte)(Vid >> 8), (byte)Vid, (byte)(Pid >> 8), (byte)Pid, (byte)(Version >> 8), (byte)Version };
        }
    }
}
