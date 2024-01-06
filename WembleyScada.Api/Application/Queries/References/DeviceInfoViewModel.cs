using WembleyScada.Api.Application.Queries.DeviceReferences;

namespace WembleyScada.Api.Application.Queries.References;

public class DeviceInfoViewModel
{
    public string DeviceId { get; set; }
    public List<PersonWorkingViewModel> Persons { get; set; }
    public List<MFCViewModel> MFCs { get; set; }

    public DeviceInfoViewModel(string deviceId, List<PersonWorkingViewModel> persons, List<MFCViewModel> mFCs)
    {
        DeviceId = deviceId;
        Persons = persons;
        MFCs = mFCs;
    }
}
