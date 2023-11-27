namespace WembleyScada.Api.Application.Queries.Products;

public class ProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string DeviceType { get; set; }

    public ProductViewModel(int id, string productName, string deviceType)
    {
        Id = id;
        ProductName = productName;
        DeviceType = deviceType;
    }
}
