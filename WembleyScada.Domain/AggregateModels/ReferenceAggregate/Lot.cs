namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Lot
{
    public int Id { get; private set; }
    public string LotId { get; private set; }
    public int LotSize { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Lot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Lot(string lotId, int lotSize)
    {
        LotId = lotId;
        LotSize = lotSize;
    }
}
