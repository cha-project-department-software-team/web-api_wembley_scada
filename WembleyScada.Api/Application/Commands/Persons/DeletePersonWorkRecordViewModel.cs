using System.Runtime.Serialization;

namespace WembleyScada.Api.Application.Commands.Persons;

[DataContract]
public class DeletePersonWorkRecordViewModel
{
    [DataMember]
    public List<string> PersonIds { get; set; }

    public DeletePersonWorkRecordViewModel(List<string> personIds)
    {
        PersonIds = personIds;
    }
}
