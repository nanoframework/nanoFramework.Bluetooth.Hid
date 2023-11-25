namespace nanoFramework.Bluetooth.HID
{
    public abstract class Device
    {
        protected abstract byte[] GetReportMap();
    }
}
