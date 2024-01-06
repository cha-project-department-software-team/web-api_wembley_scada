using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Infrastructure.Repositories;
public class DeviceRepository : BaseRepository, IDeviceRepository
{
    public DeviceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Device?> GetAsync(string deviceId)
    {
        return await _context.Devices
            .Include(x => x.WorkRecords)
            .FirstOrDefaultAsync(x => x.DeviceId == deviceId);
    }

    public async Task<IEnumerable<Device>> GetByTypeAsync(string type)
    {
        return await _context.Devices
             .Include(x => x.WorkRecords)
             .Where(x => x.DeviceType == type)
             .ToListAsync();
    }

    public async Task<IEnumerable<Device>> GetAllDevice()
    {
        return await _context.Devices
            .Include(x => x.WorkRecords)
            .ToListAsync();
    }
}
