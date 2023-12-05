using WembleyScada.Api.Application.Queries.DeviceReferences;

namespace WembleyScada.Api.Application.Queries.References;

public class ParameterViewModel
{
    public string DeviceType { get; set; }
    public string ProductName { get; set; }
    public string RefName { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }
    public List<DeviceReferenceViewModel> Devices { get; set; }

    public ParameterViewModel(string deviceType, string productName, string refName, string lotId, int lotSize, List<DeviceReferenceViewModel> devices)
    {
        DeviceType = deviceType;
        ProductName = productName;
        RefName = refName;
        LotId = lotId;
        LotSize = lotSize;
        Devices = devices;
    }
}
