using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class UpdateLotCommand : IRequest<bool>
{
    public string RefName { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }

    public UpdateLotCommand(string refName, string lotId, int lotSize)
    {
        RefName = refName;
        LotId = lotId;
        LotSize = lotSize;
    }
}
