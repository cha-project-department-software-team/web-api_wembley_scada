namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportLatestDetailsQuery : IRequest<IEnumerable<ShotOEEViewModel>>
{
    public string? DeviceId { get; set; }
    public int Interval { get; set; }
}
