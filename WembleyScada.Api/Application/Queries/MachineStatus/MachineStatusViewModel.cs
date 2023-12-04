using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

namespace WembleyScada.Api.Application.Queries.MachineStatus;

public class MachineStatusViewModel
{
    public string DeviceId { get; set; }
    public EMachineStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Timestamp { get; set; }

    public MachineStatusViewModel(string deviceId, EMachineStatus status, DateTime date, int shiftNumber, DateTime timestamp)
    {
        DeviceId = deviceId;
        Status = status;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }
}
    
