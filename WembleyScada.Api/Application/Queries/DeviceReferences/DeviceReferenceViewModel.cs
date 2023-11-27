namespace WembleyScada.Api.Application.Queries.DeviceReferences;

public class DeviceReferenceViewModel
{
    public string DeviceId { get; set; }
    public List<MFCViewModel> MFCs { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DeviceReferenceViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public DeviceReferenceViewModel(string deviceId, List<MFCViewModel> mFCs)
    {
        DeviceId = deviceId;
        MFCs = mFCs;
    }
}
