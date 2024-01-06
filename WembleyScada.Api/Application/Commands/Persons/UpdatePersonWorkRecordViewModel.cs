using System.Runtime.Serialization;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

[DataContract]
public class UpdatePersonWorkRecordViewModel
{
    [DataMember]
    public List<string> PersonIds { get; set; }

    public UpdatePersonWorkRecordViewModel(List<string> personIds)
    {
        PersonIds = personIds;
    }
}
