using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

namespace WembleyScada.Infrastructure.Repositories;

public class ErrorInformationRepository : BaseRepository, IErrorInformationRepository
{
    public ErrorInformationRepository(ApplicationDbContext context) : base(context)
    { 
    }

    public async Task<ErrorInformation?> GetAsync(string errorId)
    {
        return await _context.ErrorInformations
            .Include(x => x.ErrorStatuses)
            .FirstOrDefaultAsync(x => x.ErrorId == errorId);
    }

    public async Task<List<ErrorInformation>> GetByDeviceAsync(string deviceId)
    {
        return await _context.ErrorInformations
           .Include(x => x.ErrorStatuses)
           .Where(x => x.DeviceId == deviceId)
           .ToListAsync();
    }
}
