using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Domain.AggregateModels.ProductAggregate;

public class Product : IAggregateRoot
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string DeviceType { get; set; }
    public List<Device> Devices { get; set; }
    public List<Reference> References { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Product(int id, string productName, string deviceType, List<Device> devices, List<Reference> references)
    {
        Id = id;
        ProductName = productName;
        DeviceType = deviceType;
        Devices = devices;
        References = references;
    }
}
