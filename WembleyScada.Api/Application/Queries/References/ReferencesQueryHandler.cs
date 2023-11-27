using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.References;

public class ReferencesQueryHandler : IRequestHandler<ReferencesQuery, IEnumerable<ReferenceViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReferencesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReferenceViewModel>> Handle(ReferencesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.References
            .Include(x => x.Product)
            .AsNoTracking();
        
        if (request.DeviceType is not null)
        {
            queryable = queryable.Where(x => x.DeviceType == request.DeviceType);
        }    
        if (request.ProductId is not null)
        {
            queryable = queryable.Where(x => x.Product.Id == request.ProductId);
        }

        var references = await queryable.ToListAsync();
        return _mapper.Map<IEnumerable<ReferenceViewModel>>(references);
    }
}
