namespace nanoFramework.Bluetooth.HID
{
    public sealed class DeviceInformation
    {
        public string Manufacturer { get; }

        public string ModelNumber { get; }

        public string SerialNumber { get; }

        public string HardwareRevision { get; }

        public string FirmwareRevision { get; }

        public string SoftwareRevision { get; }

        public DeviceInformation(
            string manufacturer,
            string modelNumber = null,
            string serialNumber = null,
            string hardwareRevision = null,
            string firmwareRevision = null,
            string softwareRevision = null
            )
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
