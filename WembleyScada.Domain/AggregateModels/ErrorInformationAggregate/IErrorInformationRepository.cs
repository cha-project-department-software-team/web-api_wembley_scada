namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public interface IErrorInformationRepository : IRepository<ErrorInformation>
{
    public Task<ErrorInformation?> GetAsync(string errorId);
    public Task<List<ErrorInformation>> GetByDeviceAsync(string deviceId);
}
