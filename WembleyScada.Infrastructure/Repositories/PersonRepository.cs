using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Infrastructure.Repositories;

public class PersonRepository : BaseRepository, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddAsync(Person person)
    {
        if (!await ExistsAsync(person.PersonId))
        {
            await _context.Persons.AddAsync(person);
        }
    }

    public async Task<bool> ExistsAsync(string personId)
    {
        return await _context.Persons
            .AnyAsync(x => x.PersonId == personId);
    }

    public async Task<Person?> GetAsync(string personId)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(x => x.PersonId == personId);
    }

    public async Task DeleteAsync(string personId)
    {
        var person = await _context.Persons
            .FirstOrDefaultAsync(x => x.PersonId == personId);

        if (person is not null)
        {
            _context.Persons.Remove(person);
        }
    }
}
