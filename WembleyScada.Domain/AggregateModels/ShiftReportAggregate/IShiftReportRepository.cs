namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
public interface IShiftReportRepository: IRepository<ShiftReport>
{
    public Task AddAsync(ShiftReport shiftReport);
    public Task<ShiftReport?> GetAsync(string deviceId, int shiftNumber, DateTime date);
    public Task<ShiftReport?> GetLatestAsync(string deviceId);
    public Task<bool> ExistsAsync(string deviceId, int shiftNumber, DateTime date);
}
