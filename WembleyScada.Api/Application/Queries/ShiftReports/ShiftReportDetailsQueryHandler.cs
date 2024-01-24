using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportDetailsQueryHandler : IRequestHandler<ShiftReportDetailsQuery, IEnumerable<ShiftReportDetailViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShiftReportDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShiftReportDetailViewModel>> Handle(ShiftReportDetailsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
            .Include(x => x.Shots)
            .AsNoTracking();

        if (request.ShiftReportId is not null)
        {
            queryable = queryable.Where(x => x.Id == request.ShiftReportId);
        }

        if (request.Date is not null && request.ShiftNumber is not null)
        {
            queryable = queryable.Where(x => x.Date == request.Date
                                    && x.ShiftNumber == request.ShiftNumber);
        }

        var shiftReports = await queryable.ToListAsync();
        shiftReports.ForEach(x =>
            x.Shots = x.Shots.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList());
        
        return _mapper.Map<IEnumerable<ShiftReportDetailViewModel>>(shiftReports);
    }
}
