namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public interface IErrorInformationRepository : IRepository<ErrorInformation>
{
    public Task<ErrorInformation?> GetAsync(string errorId);
}
