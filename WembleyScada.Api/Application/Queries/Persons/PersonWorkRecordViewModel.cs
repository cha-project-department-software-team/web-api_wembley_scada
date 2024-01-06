using WembleyScada.Api.Application.Queries.Devices;

namespace WembleyScada.Api.Application.Queries.Persons;

public class PersonWorkRecordViewModel
{
    public DeviceViewModel Device { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime Date { get; set; }

    public PersonWorkRecordViewModel(DeviceViewModel device, DateTime startTime, DateTime date)
    {
        Device = device;
        StartTime = startTime;
        Date = date;
    }
}
