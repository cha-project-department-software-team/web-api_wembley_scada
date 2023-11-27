namespace WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

public class MFC : IAggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public string DeviceId { get; set; }
    public int ReferenceId { get; set; }

    public MFC(int id, string name, double value, string deviceId, int referenceId)
    {
        Id = id;
        Name = name;
        Value = value;
        DeviceId = deviceId;
        ReferenceId = referenceId;
    }
}
