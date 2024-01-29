namespace WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

public interface IDeviceReferenceRepository : IRepository<DeviceReference>
{
    public Task<DeviceReference?> GetAsync(int referenceId, string deviceId);
    public Task<DeviceReference?> GetByRefIdAsync(int referenceId);
}
