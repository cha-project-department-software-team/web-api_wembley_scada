using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.DeviceReferences;

public class DeviceReferencesQueryHandler : IRequestHandler<DeviceReferencesQuery, IEnumerable<DeviceReferenceViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeviceReferencesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceReferenceViewModel>> Handle(DeviceReferencesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.DeviceReferences
            .Include(x => x.Device)
            .Include(x => x.Reference)
            .Include(x => x.MFCs)
            .Where(x => x.ReferenceId == request.ReferenceId)
            .AsNoTracking();

        if (request.DeviceId is not null)
        {
            queryable = queryable.Where(x => x.DeviceId == request.DeviceId);
        }

        var deviceReferences = await queryable.ToListAsync();
        return _mapper.Map<IEnumerable<DeviceReferenceViewModel>>(deviceReferences);
    }
}
