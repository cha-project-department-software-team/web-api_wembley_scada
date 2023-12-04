namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusesQuery : IRequest<IEnumerable<ErrorStatusViewModel>>
{
    public string DeviceId { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}
