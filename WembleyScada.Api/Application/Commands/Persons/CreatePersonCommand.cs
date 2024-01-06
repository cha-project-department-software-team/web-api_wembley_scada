using System.Runtime.Serialization;

namespace WembleyScada.Api.Application.Commands.Persons;

[DataContract]
public class CreatePersonCommand : IRequest<bool>
{

    [DataMember]
    public string PersonId { get; set; }
    [DataMember]
    public string PersonName { get; set; }

    public CreatePersonCommand(string personId, string personName)
    {
        PersonId = personId;
        PersonName = personName;
    }
}
