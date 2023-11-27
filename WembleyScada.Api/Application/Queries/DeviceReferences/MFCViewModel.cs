namespace WembleyScada.Api.Application.Queries.DeviceReferences;

public class MFCViewModel
{
    public string Name { get; set; }
    public double Value { get; set; }

    public MFCViewModel(string name, double value)
    {
        Name = name;
        Value = value;
    }
}
