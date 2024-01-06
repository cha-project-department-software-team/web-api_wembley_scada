using System.Runtime.Serialization;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

[DataContract]
public class UpdateLotViewModel
{
    [DataMember]
    public string LotId { get; set; }
    [DataMember]
    public int LotSize { get; set; }
    [DataMember]
    public ELotStatus? LotStatus { get; set; }
    [DataMember]
    public DateTime? EndTime { get; set; }

    public UpdateLotViewModel(string lotId, int lotSize, ELotStatus? lotStatus, DateTime? endTime)
    {
        LotId = lotId;
        LotSize = lotSize;
        LotStatus = lotStatus;
        EndTime = endTime;
    }
}
