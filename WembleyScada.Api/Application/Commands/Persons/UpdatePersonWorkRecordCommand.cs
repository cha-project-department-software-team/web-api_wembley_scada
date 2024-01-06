namespace WembleyScada.Api.Application.Commands.Persons;

public class UpdatePersonWorkRecordCommand : IRequest<bool>
{
    public string DeviceId { get; set; }
    public List<string> PersonIds { get; set; }

    public UpdatePersonWorkRecordCommand(string deviceId, List<string> personIds)
    {
        DeviceId = deviceId;
        PersonIds = personIds;
    }
}
