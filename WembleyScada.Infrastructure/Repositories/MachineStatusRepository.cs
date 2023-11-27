using System.Data;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

namespace WembleyScada.Infrastructure.Repositories;
public class MachineStatusRepository : BaseRepository, IMachineStatusRepository
{
    public MachineStatusRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task AddAsync(MachineStatus machineStatus)
    {
        if (!await ExistsAsync(machineStatus.DeviceId, machineStatus.Timestamp))
        {
            await _context.MachineStatus.AddAsync(machineStatus);
        }
    }

    public async Task<bool> ExistsAsync(string deviceId, DateTime timestamp)
    {
        return await _context.MachineStatus
            .AnyAsync(x => x.DeviceId == deviceId
                      && x.Timestamp == timestamp);
    }

    public async Task<MachineStatus?> GetLatestAsync(string deviceId)
    {
        return await _context.MachineStatus
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
    }
}