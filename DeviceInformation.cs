// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.Bluetooth.Hid
{
    /// <summary>
    /// A class representing the various information about the device to advertise.
    /// </summary>
    public class DeviceInformation
    {
        /// <summary>
        /// Gets the device manufacturer name.
        /// </summary>
        public string Manufacturer { get; }

        /// <summary>
        /// Gets the device model number.
        /// </summary>
        public string ModelNumber { get; }

        /// <summary>
        /// Gets the device serial number.
        /// </summary>
        public string SerialNumber { get; }

        /// <summary>
        /// Gets the device hardware revision number.
        /// </summary>
        public string HardwareRevision { get; }

        /// <summary>
        /// Gets the device firmware revision number.
        /// </summary>
        public string FirmwareRevision { get; }

        /// <summary>
        /// Gets the software version number.
        /// </summary>
        public string SoftwareRevision { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInformation"/> class.
        /// </summary>
        /// <param name="manufacturer">The name of the device manufacturer.</param>
        /// <param name="modelNumber">The device model number.</param>
        /// <param name="serialNumber">The device serial number.</param>
        /// <param name="hardwareRevision">The device hardware revision number.</param>
        /// <param name="firmwareRevision">The device firmware revision number.</param>
        /// <param name="softwareRevision">The software version number.</param>
        public DeviceInformation(
            string manufacturer,
            string modelNumber = null,
            string serialNumber = null,
            string hardwareRevision = null,
            string firmwareRevision = null,
            string softwareRevision = null)
        {
            Manufacturer = manufacturer;
            ModelNumber = modelNumber;
            SerialNumber = serialNumber;
            HardwareRevision = hardwareRevision;
            FirmwareRevision = firmwareRevision;
            SoftwareRevision = softwareRevision;
        }
    }
}
