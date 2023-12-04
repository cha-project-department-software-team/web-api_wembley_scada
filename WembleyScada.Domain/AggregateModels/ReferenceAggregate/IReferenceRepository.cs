namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public interface IReferenceRepository : IRepository<Reference>
{
    public Task<Reference?> GetAsync(string refName);
}
