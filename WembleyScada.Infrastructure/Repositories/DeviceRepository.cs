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
            .FirstOrDefaultAsync(x => x.DeviceId == deviceId);
    }

    public async Task<IEnumerable<Device>> GetByTypeAsync(string type)
    {
        return await _context.Devices
             .Where(c => c.DeviceType == type)
             .ToListAsync();
    }

    public async Task<IEnumerable<Device>> GetAllDevice()
    {
        return await _context.Devices.ToListAsync();
    }
}
