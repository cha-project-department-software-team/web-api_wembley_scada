using System.Runtime.Serialization;

namespace WembleyScada.Api.Application.Commands.Persons;

[DataContract]
public class CreatePersonWorkRecordViewModel
{
    [DataMember]
    public List<string> PersonIds { get; set; }

    public CreatePersonWorkRecordViewModel(List<string> personIds)
    {
        PersonIds = personIds;
    }
}
