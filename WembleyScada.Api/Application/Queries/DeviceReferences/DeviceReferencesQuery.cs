namespace WembleyScada.Api.Application.Queries.DeviceReferences;

public class DeviceReferencesQuery : IRequest<IEnumerable<DeviceReferenceViewModel>>
{
    public int ReferenceId { get; set; }
    public string? DeviceId { get; set; }
}
