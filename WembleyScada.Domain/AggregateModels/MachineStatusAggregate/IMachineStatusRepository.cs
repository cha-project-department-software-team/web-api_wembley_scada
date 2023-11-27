namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public interface IMachineStatusRepository : IRepository<MachineStatus>
{
    public Task AddAsync(MachineStatus machineStatus);
    public Task<bool> ExistsAsync(string deviceId, DateTime timestamp);
    public Task<MachineStatus?> GetLatestAsync(string deviceId);
}
