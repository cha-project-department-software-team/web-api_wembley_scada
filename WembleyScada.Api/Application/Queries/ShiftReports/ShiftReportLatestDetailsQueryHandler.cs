using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportLatestDetailsQueryHandler : IRequestHandler<ShiftReportLatestDetailsQuery, IEnumerable<ShotOEEViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMachineStatusRepository _machineStatusRepository;

    public ShiftReportLatestDetailsQueryHandler(ApplicationDbContext context, IMapper mapper, IMachineStatusRepository machineStatusRepository)
    {
        _context = context;
        _mapper = mapper;
        _machineStatusRepository = machineStatusRepository;
    }

    public async Task<IEnumerable<ShotOEEViewModel>> Handle(ShiftReportLatestDetailsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
           .Include(x => x.Shots)
           .Where(x => x.DeviceId == request.DeviceId)
           .OrderByDescending(x => x.Date)
           .ThenByDescending(x => x.ShiftNumber)
           .AsNoTracking();

        var latestShiftReport = await queryable.FirstOrDefaultAsync();
        if (latestShiftReport is null)
        {
            throw new Exception($"Don't have any Report in Device {request.DeviceId}");
        }

        var shots = latestShiftReport.Shots;

        if (request.DeviceId is not null)
        {
            var latestStatus = await _machineStatusRepository.GetLatestAsync(request.DeviceId);
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Off)
            {
                shots.Clear();
            }
        }

        if (request.Interval != 1)
        {
            shots = shots.Where((x, index) => (index + 1) % request.Interval == 1).ToList();
        }

        return _mapper.Map<IEnumerable<ShotOEEViewModel>>(shots);
    }
}
