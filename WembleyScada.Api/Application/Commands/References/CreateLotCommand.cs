namespace WembleyScada.Api.Application.Commands.References;

public class CreateLotCommand : IRequest<bool>
{
    public string RefName { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }

    public CreateLotCommand(string refName, string lotId, int lotSize)
    {
        RefName = refName;
        LotId = lotId;
        LotSize = lotSize;
    }
}
