using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

public class DeviceReference : IAggregateRoot
{
    public int ReferenceId { get; set; }
    public Reference Reference { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public List<MFC> MFCs { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DeviceReference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    public DeviceReference(int referenceId, Reference reference, string deviceId, Device device, List<MFC> mFCs)
    {
        ReferenceId = referenceId;
        Reference = reference;
        DeviceId = deviceId;
        Device = device;
        MFCs = mFCs;
    }

    public void UpdateMFC(List<MFC> mFCs)
    {
        MFCs = mFCs;
    }
}
