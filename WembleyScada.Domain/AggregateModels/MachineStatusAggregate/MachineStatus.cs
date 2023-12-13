using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public class MachineStatus : IAggregateRoot
{
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Date { get; set; }
    public EMachineStatus Status { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MachineStatus() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MachineStatus(int id, string deviceId, Device device, int shiftNumber, DateTime date, EMachineStatus status, DateTime timestamp)
    {
        Id = id;
        DeviceId = deviceId;
        Device = device;
        ShiftNumber = shiftNumber;
        Date = date;
        Status = status;
        Timestamp = timestamp;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MachineStatus(Device device, EMachineStatus status, DateTime timestamp)
    {
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);
        var date = ShiftTimeHelper.GetShiftDate(timestamp);
        Device = device;
        ShiftNumber = shiftNumber;
        Date = date;
        Status = status;
        Timestamp = timestamp;
    }

    public MachineStatus(Device device, EMachineStatus status, int shiftNumber, DateTime date, DateTime timestamp)
    {
        Device = device;
        Status = status;
        ShiftNumber = shiftNumber;
        Date = date;
        Timestamp = timestamp;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
