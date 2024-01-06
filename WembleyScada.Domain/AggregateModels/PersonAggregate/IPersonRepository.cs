namespace WembleyScada.Domain.AggregateModels.PersonAggregate;

public interface IPersonRepository : IRepository<Person>
{
    public Task AddAsync(Person person);
    public Task<bool> ExistsAsync(string personId);
    public Task<Person?> GetAsync(string personId);
    public Task DeleteAsync(string personId);
}
