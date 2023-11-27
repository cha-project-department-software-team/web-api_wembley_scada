using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.Products;

public class ProductsQueryHandler : IRequestHandler<ProductsQuery, IEnumerable<ProductViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> Handle(ProductsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Products.AsNoTracking();

        if (request.DeviceType is not null)
        {
            queryable = queryable.Where(x => x.DeviceType == request.DeviceType);
        }

        var products = await queryable.ToListAsync();
        return _mapper.Map<IEnumerable<ProductViewModel>>(products);
    }
}
