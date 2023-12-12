// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using nanoFramework.Bluetooth.HID.Extensions;
using nanoFramework.Device.Bluetooth;

namespace nanoFramework.Bluetooth.HID
{
    /// <summary>
    /// The Plug and Play Information.
    /// </summary>
    public sealed class PnpElements
    {
        /// <summary>
        /// Gets the PnP Signature.
        /// </summary>
        public byte Sig { get; }

        /// <summary>
        /// Gets the PnP Vendor ID.
        /// </summary>
        public ushort Vid { get; }

        /// <summary>
        /// Gets the PnP Product ID.
        /// </summary>
        public ushort Pid { get; }

        /// <summary>
        /// Gets the PnP version.
        /// </summary>
        public ushort Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PnpElements"/> class.
        /// </summary>
        /// <param name="sig">The signature.</param>
        /// <param name="vid">The vendor ID.</param>
        /// <param name="pid">The product ID.</param>
        /// <param name="version">The version number.</param>
        public PnpElements(byte sig, ushort vid, ushort pid, ushort version)
        {
            Sig = sig;
            Vid = vid;
            Pid = pid;
            Version = version;
        }

        /// <summary>
        /// Serializes the current instance into a PnP byte array specially formatted for advertising the device.
        /// </summary>
        /// <returns>A specially formatted byte array representing this instance.</returns>
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

        /// <summary>
        /// Serializes the current instance into a PnP byte array specially formatted for advertising the device and wraps it in a <see cref="Buffer"/> instance.
        /// </summary>
        /// <returns>A specially formatted byte array representing this instance wrapped in a <see cref="Buffer"/> instance.</returns>
        public Buffer ToBuffer()
        {
            return this.Serialize()
                .AsBuffer();
        }
    }
}
