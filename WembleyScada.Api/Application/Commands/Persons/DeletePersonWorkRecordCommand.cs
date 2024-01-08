namespace WembleyScada.Api.Application.Commands.Persons;

public class DeletePersonWorkRecordCommand : IRequest<bool>
{
    public string DeviceId { get; set; }
    public List<string> PersonIds { get; set; }

    public DeletePersonWorkRecordCommand(string deviceId, List<string> personIds)
    {
        DeviceId = deviceId;
        PersonIds = personIds;
    }
}
