using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class UpdateLotCommand : IRequest<bool>
{
    public string RefName { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }
    public ELotStatus LotStatus { get; set; }
    public DateTime? EndTime { get; set; }

    public UpdateLotCommand(string refName, string lotId, int lotSize, ELotStatus lotStatus, DateTime? endTime)
    {
        RefName = refName;
        LotId = lotId;
        LotSize = lotSize;
        LotStatus = lotStatus;
        EndTime = endTime;
    }
}
