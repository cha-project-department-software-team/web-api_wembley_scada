namespace WembleyScada.Api.Application.Queries.Devices;

public class DeviceViewModel
{
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public int DisplayPriority { get; set; }

    public DeviceViewModel(string deviceId, string deviceName, string deviceType, int displayPriority)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        DeviceType = deviceType;
        DisplayPriority = displayPriority;
    }
}
