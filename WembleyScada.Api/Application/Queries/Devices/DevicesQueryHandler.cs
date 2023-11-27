using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.Devices;

public class DevicesQueryHandler : IRequestHandler<DevicesQuery, IEnumerable<DeviceViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DevicesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceViewModel>> Handle(DevicesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Devices.AsNoTracking();

        if (request.DeviceType is not null)
        {
            queryable = queryable.Where(x => x.DeviceType == request.DeviceType);
        }

        var devices = await queryable.ToListAsync();
        return _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
    }
}
