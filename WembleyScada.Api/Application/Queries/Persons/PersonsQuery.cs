namespace WembleyScada.Api.Application.Queries.Persons;

public class PersonsQuery : IRequest<IEnumerable<PersonViewModel>>
{
    public string? PersonId { get; set; }
}
