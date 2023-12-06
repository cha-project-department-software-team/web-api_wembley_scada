namespace WembleyScada.Api.Application.Commands.DeviceReferences;

public class UpdateDeviceReferenceCommand : IRequest<bool>
{
    public int ReferenceId { get; set; }
    public string DeviceId { get; set; }
    public List<UpdateMFCViewModel> MFCs { get; set; }

    public UpdateDeviceReferenceCommand(int referenceId, string deviceId, List<UpdateMFCViewModel> mFCs)
    {
        ReferenceId = referenceId;
        DeviceId = deviceId;
        MFCs = mFCs;
    }
}
