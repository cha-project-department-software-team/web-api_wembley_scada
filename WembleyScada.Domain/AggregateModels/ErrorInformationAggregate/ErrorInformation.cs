using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public class ErrorInformation : IAggregateRoot
{
    public int Id { get; set; }
    public string ErrorId { get; set; }
    public string ErrorName { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ErrorInformation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    public ErrorInformation(int id, string errorId, string errorName, string deviceId, Device device, int shiftNumber, DateTime date, DateTime timestamp)
    {
        Id = id;
        ErrorId = errorId;
        ErrorName = errorName;
        DeviceId = deviceId;
        Device = device;
        ShiftNumber = shiftNumber;
        Date = date;
        Timestamp = timestamp;
    }
}
