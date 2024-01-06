using WembleyScada.Domain.AggregateModels.ProductAggregate;

namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Reference : IAggregateRoot
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string RefName { get; set; }
    public string DeviceType { get; set; }
    public List<Lot> Lots { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Reference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Reference(int id, int productId, Product product, string refName, string deviceType, List<Lot> lots)
    {
        Id = id;
        ProductId = productId;
        Product = product;
        RefName = refName;
        DeviceType = deviceType;
        Lots = lots;
    }

    public void AddLot(string lotId, int lotSize, ELotStatus lotStatus, DateTime startTime)
    {
        var lot = new Lot(lotId, lotSize, lotStatus, startTime);
        if (Lots.Any(d => d.LotId == lotId))
        {
            throw new ChildEntityDuplicationException(lotId, lot, Id, this);
        }

        Lots.Add(lot);
    }

    public void UpdateLot(string lotId, int lotSize, ELotStatus? lotStatus, DateTime? endTime)
    {
        var lot = Lots.FirstOrDefault(x => x.LotStatus == ELotStatus.Working);
        if (lot is not null)
        {
            lot.Update(lotId, lotSize, lotStatus, endTime);
        }
    }
}
