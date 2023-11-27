namespace WembleyScada.Api.Application.Queries.References;

public class ReferencesQuery : IRequest<IEnumerable<ReferenceViewModel>>
{
    public int? ProductId { get; set; }
    public string? DeviceType { get; set; }
}
