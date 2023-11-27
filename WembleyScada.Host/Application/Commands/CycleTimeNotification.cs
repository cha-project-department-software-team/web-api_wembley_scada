namespace WembleyScada.Host.Application.Commands;
public class CycleTimeNotification: INotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public double CycleTime { get; set; }
    public DateTime Timestamp { get; set; }

    public CycleTimeNotification(string deviceType, string deviceId, double cycleTime, DateTime timestamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        CycleTime = cycleTime;
        Timestamp = timestamp;
    }
}
