using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ProductAggregate;

namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Reference : IAggregateRoot
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string RefName { get; set; }
    public string DeviceType { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Reference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Reference(int id, int productId, Product product, string refName, string deviceType)
    {
        Id = id;
        ProductId = productId;
        Product = product;
        RefName = refName;
        DeviceType = deviceType;
    }
}
