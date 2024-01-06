using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Domain.AggregateModels.PersonAggregate;

public class Person : IAggregateRoot
{
    public string PersonId { get; set; }
    public string PersonName { get; set; }
    public List<PersonWorkRecord> WorkRecords { get; set; } = new List<PersonWorkRecord>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Person() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Person(string personId, string personName)
    {
        PersonId = personId;
        PersonName = personName;
    }

    public void AddPersonWorkRecord(Device device, EWorkStatus workStatus, DateTime startTime)
    {
        var personWorkRecord = new PersonWorkRecord(device, workStatus, startTime);
        WorkRecords.Add(personWorkRecord);
    }

    public void Update(Device device, EWorkStatus? workStatus, DateTime? endTime)
    {
        var personWorkRecord = WorkRecords.Find(x => x.WorkStatus == EWorkStatus.Working);
        if (personWorkRecord is not null)
        {
            personWorkRecord.Update(workStatus, endTime);
        }
    }
}
