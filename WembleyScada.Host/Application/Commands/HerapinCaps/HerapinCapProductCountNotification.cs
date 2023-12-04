namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapProductCountNotification : INotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public int ProductCount { get; set; }
    public DateTime Timestamp { get; set; }

    public HerapinCapProductCountNotification(string deviceType, string deviceId, int productCount, DateTime timestamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        ProductCount = productCount;
        Timestamp = timestamp;
    }
}
