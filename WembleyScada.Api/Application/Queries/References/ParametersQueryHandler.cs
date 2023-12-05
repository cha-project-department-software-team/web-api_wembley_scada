using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Api.Application.Queries.DeviceReferences;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.References;

public class ParametersQueryHandler : IRequestHandler<ParametersQuery, IEnumerable<ParameterViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ParametersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ParameterViewModel>> Handle(ParametersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.References
            .Include(x => x.Product)
            .Include(x => x.Lots)
            .AsNoTracking();

        if (request.DeviceType is not null)
        {
            queryable = queryable.Where(x => x.DeviceType == request.DeviceType);
        } 

        var references = await queryable.ToListAsync();

        references = references.GroupBy(x => x.DeviceType)
            .Select(group => group.OrderByDescending(x => x.Lots.Any() ? x.Lots.Max(x => x.Timestamp) : DateTime.MinValue).First())
            .ToList();

        var viewModels = new List<ParameterViewModel>();
        foreach (var reference in references)
        {
            var lot = reference.Lots.OrderByDescending(x => x.Timestamp).First();

            var deviceReferences = await _context.DeviceReferences
                .Include(x => x.Device)
                .Include(x => x.Reference)
                .Include(x => x.MFCs)
                .Where(x => x.ReferenceId == reference.Id)
                .ToListAsync();

            var viewModel = new ParameterViewModel(
                reference.DeviceType,
                reference.Product.ProductName,
                reference.RefName,
                lot.LotId,
                lot.LotSize,
                _mapper.Map<List<DeviceReferenceViewModel>>(deviceReferences));

            viewModels.Add(viewModel);
        }

        return viewModels;
    }
}
