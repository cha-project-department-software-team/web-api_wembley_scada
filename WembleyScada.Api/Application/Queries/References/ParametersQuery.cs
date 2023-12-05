namespace WembleyScada.Api.Application.Queries.References;

public class ParametersQuery : IRequest<IEnumerable<ParameterViewModel>>
{
    public string? DeviceType { get; set; }
    public int? ReferenceId { get; set; }
}
