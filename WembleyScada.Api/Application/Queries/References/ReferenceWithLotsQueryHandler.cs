using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Api.Application.Queries.DeviceReferences;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.References;

public class ReferenceWithLotsQueryHandler : IRequestHandler<ReferenceWithLotsQuery, ReferenceWithLotViewModel>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReferenceWithLotsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReferenceWithLotViewModel> Handle(ReferenceWithLotsQuery request, CancellationToken cancellationToken)
    {
        var reference = await _context.References
            .Include(x => x.Product)
            .Include(x => x.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.ReferenceId) ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with ID '{request.ReferenceId}' cannot be found.");

        var lot = reference.Lots
            .OrderByDescending(x => x.Id)
            .FirstOrDefault() ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Lot)}' cannot be found.");

        var deviceRefencens = await _context.DeviceReferences
            .Include(x => x.Device)
            .Include(x => x.Reference)
            .Include(x => x.MFCs)
            .Where(x => x.ReferenceId == request.ReferenceId)
            .ToListAsync();

        return new ReferenceWithLotViewModel(
            reference.DeviceType,
            reference.Product.ProductName,
            reference.RefName,
            lot.LotId,
            lot.LotSize,
            _mapper.Map<List<DeviceReferenceViewModel>>(deviceRefencens));
    }
}
