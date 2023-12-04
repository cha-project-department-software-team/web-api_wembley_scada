using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapMachineStatusChangedNotification : INotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public EMachineStatus MachineStatus { get; set; }
    public DateTime Timestamp { get; set; }

    public HerapinCapMachineStatusChangedNotification(string deviceType, string deviceId, EMachineStatus machineStatus, DateTime timestamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        MachineStatus = machineStatus;
        Timestamp = timestamp;
    }
}
