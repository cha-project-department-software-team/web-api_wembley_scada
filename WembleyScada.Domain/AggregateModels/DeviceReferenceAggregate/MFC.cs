namespace WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

public class MFC
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public string DeviceId { get; set; }
    public int ReferenceId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MFC() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MFC(int id, string name, double value, double minValue, double maxValue, string deviceId, int referenceId)
    {
        Id = id;
        Name = name;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
        DeviceId = deviceId;
        ReferenceId = referenceId;
    }
}
