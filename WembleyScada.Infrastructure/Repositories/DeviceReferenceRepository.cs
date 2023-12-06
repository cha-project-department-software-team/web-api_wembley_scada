using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

namespace WembleyScada.Infrastructure.Repositories;

public class DeviceReferenceRepository : BaseRepository, IDeviceReferenceRepository
{
    public DeviceReferenceRepository(ApplicationDbContext context) : base(context)
    { 
    }

    public async Task<DeviceReference?> GetAsync(int referenceId, string deviceId)
    {
        return await _context.DeviceReferences
            .Include(x => x.Reference)
            .Include(x => x.Device)
            .Include(x => x.MFCs)
            .FirstOrDefaultAsync(x => x.ReferenceId == referenceId && x.DeviceId == deviceId);
    }
}
