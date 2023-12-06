namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportDetailsQuery : IRequest<IEnumerable<ShiftReportDetailViewModel>>
{
    public int? ShiftReportId { get; set; }
    public DateTime? Date { get; set; }
    public int? ShiftNumber { get; set; }
}
