using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.MachineStatus;

public class MachineStatusQueryHandler : IRequestHandler<MachineStatusQuery, IEnumerable<MachineStatusViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MachineStatusQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MachineStatusViewModel>> Handle(MachineStatusQuery request, CancellationToken cancellationToken)
    {
        var machineStatuses = await _context.MachineStatus
                .Where(x => x.DeviceId == request.DeviceId 
                           && x.Date >= request.StartTime
                           && x.Date <= request.EndTime)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();

        return _mapper.Map<IEnumerable<MachineStatusViewModel>>(machineStatuses);
    }
}
