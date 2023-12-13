using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Infrastructure.Repositories;
public class ShiftReportRepository : BaseRepository, IShiftReportRepository
{
    public ShiftReportRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddAsync(ShiftReport shiftReport)
    {
        if (!await ExistsAsync(shiftReport.DeviceId, shiftReport.ShiftNumber, shiftReport.Date))
        {
            await _context.ShiftReports.AddAsync(shiftReport);
        }
    }

    public async Task<bool> ExistsAsync(string deviceId, int shiftNumber, DateTime date)
    {
        return await _context.ShiftReports
            .AnyAsync(x => x.DeviceId == deviceId
                           && x.ShiftNumber == shiftNumber
                           && x.Date == date);
    }

    public async Task<ShiftReport?> GetAsync(string deviceId, int shiftNumber, DateTime date)
    {
        return await _context.ShiftReports
            .Include(x => x.Shots)
            .Include(x => x.Device)
            .FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.ShiftNumber == shiftNumber && x.Date == date);
    }

    public async Task<ShiftReport?> GetLatestAsync(string deviceId)
    {
        return await _context.ShiftReports
            .Include(x => x.Shots)
            .Include(x => x.Device)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.ShiftNumber)
            .FirstOrDefaultAsync();
    }
}
