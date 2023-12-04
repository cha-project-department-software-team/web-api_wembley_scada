namespace WembleyScada.Api.Application.Queries.MachineStatus;

public class MachineStatusQuery : IRequest<IEnumerable<MachineStatusViewModel>>
{
    public string DeviceId { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}
