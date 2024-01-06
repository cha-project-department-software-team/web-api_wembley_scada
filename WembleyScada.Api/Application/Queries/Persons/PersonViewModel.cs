namespace WembleyScada.Api.Application.Queries.Persons;

public class PersonViewModel
{
    public string PersonId { get; set; }
    public string PersonName { get; set; }
    public List<PersonWorkRecordViewModel> WorkRecords { get; set; }

    public PersonViewModel(string personId, string personName, List<PersonWorkRecordViewModel> workRecords)
    {
        PersonId = personId;
        PersonName = personName;
        WorkRecords = workRecords;
    }
}
