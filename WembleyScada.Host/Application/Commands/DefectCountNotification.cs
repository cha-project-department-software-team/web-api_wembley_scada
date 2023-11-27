namespace WembleyScadaThaiDuongScada.Host.Application.Commands;
public class DefectCountNotification : INotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public int DefectCount { get; set; }
    public DateTime Timestamp { get; set; }

    public DefectCountNotification(string deviceType, string deviceId, int defectCount, DateTime timestamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        DefectCount = defectCount;
        Timestamp = timestamp;
    }
}
