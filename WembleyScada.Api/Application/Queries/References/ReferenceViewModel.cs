namespace WembleyScada.Api.Application.Queries.References;

public class ReferenceViewModel
{
    public int Id { get; set; }
    public string RefName { get; set; }
    public string DeviceType { get; set; }
    public string ProductName { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ReferenceViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ReferenceViewModel(int id, string refName, string deviceType, string productName)
    {
        Id = id;
        RefName = refName;
        DeviceType = deviceType;
        ProductName = productName;
    }
}
