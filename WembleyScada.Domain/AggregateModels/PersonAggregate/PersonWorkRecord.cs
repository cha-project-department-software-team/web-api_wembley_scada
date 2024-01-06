using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Domain.AggregateModels.PersonAggregate;

public class PersonWorkRecord
{
    public int Id { get; set; }
    public string PersonId { get; set; }
    public Person Person { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public EWorkStatus WorkStatus { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PersonWorkRecord() { }

    public PersonWorkRecord(Device device, EWorkStatus workStatus, DateTime startTime)
    {
        Device = device;
        WorkStatus = workStatus;
        StartTime = startTime;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public void Update(Person person)
    {
        Person = person;
    }

    public void UpdateStatus(EWorkStatus workStatus, DateTime endTime) 
    { 
        WorkStatus = workStatus;
        EndTime = endTime;
    }
}
