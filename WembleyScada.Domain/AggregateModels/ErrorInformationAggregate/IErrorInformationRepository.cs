namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public interface IErrorInformationRepository
{
    public Task AddAsync(ErrorInformation errorInformation);
    public Task<bool> ExistsAsync(string deviceId, DateTime timestamp);
    public Task<ErrorInformation?> GetLatestAsync(string deviceId);
}
