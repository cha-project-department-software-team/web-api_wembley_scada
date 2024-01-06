namespace WembleyScada.Api.Application.Queries.References;

public class PersonWorkingViewModel
{
    public string PersonId { get; set; }
    public string PersonName { get; set; }

    public PersonWorkingViewModel(string personId, string personName)
    {
        PersonId = personId;
        PersonName = personName;
    }
}
