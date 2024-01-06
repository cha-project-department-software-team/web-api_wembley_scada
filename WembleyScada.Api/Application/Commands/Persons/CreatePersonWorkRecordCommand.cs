namespace WembleyScada.Api.Application.Commands.Persons;

public class CreatePersonWorkRecordCommand : IRequest<bool>
{
    public string DeviceId { get; set; }
    public List<string> PersonIds { get; set; }

    public CreatePersonWorkRecordCommand(string deviceId, List<string> personIds)
    {
        DeviceId = deviceId;
        PersonIds = personIds;
    }
}
