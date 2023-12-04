namespace WembleyScada.Host.Application.Commands;

public class ErrorStatusNotification : INotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public string ErrorId { get; set; }
    public int Value { get; set; }
    public DateTime Timestamp { get; set; }

    public ErrorStatusNotification(string deviceType, string deviceId, string errorId, int value, DateTime timestamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        ErrorId = errorId;
        Value = value;
        Timestamp = timestamp;
    }
}
