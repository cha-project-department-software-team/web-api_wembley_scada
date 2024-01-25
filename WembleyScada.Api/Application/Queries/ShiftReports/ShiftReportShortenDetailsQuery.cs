namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportShortenDetailsQuery : IRequest<IEnumerable<ShiftReportDetailViewModel>>
{
    public int? ShiftReportId { get; set; }
    public string? DeviceId { get; set; }
    public DateTime? Date { get; set; }
    public int? ShiftNumber { get; set; }
    public int Interval { get; set; }
}
