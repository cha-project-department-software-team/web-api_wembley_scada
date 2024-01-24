namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportDetailsQuery : PaginatedQuery, IRequest<IEnumerable<ShiftReportDetailViewModel>>
{
    public int? ShiftReportId { get; set; }
    public DateTime? Date { get; set; }
    public int? ShiftNumber { get; set; }
}
