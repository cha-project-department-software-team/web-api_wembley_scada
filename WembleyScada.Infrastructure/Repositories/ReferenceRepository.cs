using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Infrastructure.Repositories;

public class ReferenceRepository : BaseRepository, IReferenceRepository
{
    public ReferenceRepository(ApplicationDbContext context) : base(context)
    { 
    }

    public async Task<Reference?> GetAsync(string refName)
    {
        return await _context.References
            .Include(x => x.Lots)
            .FirstOrDefaultAsync(x => x.RefName == refName);
    }

    public async Task<IEnumerable<Reference>> GetByTypeAsync(string deviceType)
    {
        return await _context.References
            .Include(x => x.Lots)
            .Where(x => x.DeviceType == deviceType)
            .ToListAsync();
    }
}
