namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class DownloadReportsQuery : IRequest<byte[]>
{
    public string DeviceId { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}
