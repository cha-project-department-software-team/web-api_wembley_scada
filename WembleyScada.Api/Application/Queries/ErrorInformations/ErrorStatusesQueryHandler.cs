using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusesQueryHandler : IRequestHandler<ErrorStatusesQuery, IEnumerable<ErrorStatusViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ErrorStatusesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ErrorStatusViewModel>> Handle(ErrorStatusesQuery request, CancellationToken cancellationToken)
    {
        var errorInformations = await _context.ErrorInformations
            .Include(x => x.ErrorStatuses)
            .Where(x => x.DeviceId == request.DeviceId)
            .ToListAsync();

        var errorStatus = errorInformations.SelectMany(x =>
            x.ErrorStatuses.Where(x => x.Date >= request.StartTime
                                    && x.Date <= request.EndTime
                                    && x.Value == 1)
                           .OrderByDescending(x => x.Timestamp));

        return _mapper.Map<IEnumerable<ErrorStatusViewModel>>(errorStatus);
    }
}
