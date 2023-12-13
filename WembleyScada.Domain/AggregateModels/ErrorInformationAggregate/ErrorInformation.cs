using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public class ErrorInformation : IAggregateRoot
{
    public string ErrorId { get; set; }
    public string ErrorName { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public List<ErrorStatus> ErrorStatuses { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ErrorInformation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ErrorInformation(string errorId, string errorName, string deviceId, Device device, List<ErrorStatus> errorStatuses)
    {
        ErrorId = errorId;
        ErrorName = errorName;
        DeviceId = deviceId;
        Device = device;
        ErrorStatuses = errorStatuses;
    }

    public void AddErrorStatus(int value, DateTime date, int shiftNumber, DateTime timestamp)
    {
        var errorStatus = new ErrorStatus(value, date, shiftNumber, timestamp);
        ErrorStatuses.Add(errorStatus);
    }
}
