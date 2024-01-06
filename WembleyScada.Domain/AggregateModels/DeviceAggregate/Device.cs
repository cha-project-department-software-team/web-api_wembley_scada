using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Domain.AggregateModels.DeviceAggregate;
public class Device: IAggregateRoot
{
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public int DisplayPriority { get; set; }
    public List<PersonWorkRecord> WorkRecords { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Device() { }

    public Device(string deviceId, string deviceName, string deviceType, int displayPriority, List<PersonWorkRecord> workRecords)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        DeviceType = deviceType;
        DisplayPriority = displayPriority;
        WorkRecords = workRecords;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public void UpdateWorkRecords(List<PersonWorkRecord> workRecords)
    {
        WorkRecords = workRecords;
    }
}
