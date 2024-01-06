using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WembleyScada.Api.Application.Queries.DeviceReferences;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;
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

        if (request.ReferenceId is not null)
        {
            queryable = queryable.Where(x => x.Id == request.ReferenceId);
        }

        var references = await queryable.ToListAsync();

        references = references.GroupBy(x => x.DeviceType)
            .Select(group => group.OrderByDescending(x => x.Lots.Any() ? x.Lots.Max(x => x.StartTime) : DateTime.MinValue).First())
            .ToList();

        var viewModels = new List<ParameterViewModel>();
        foreach (var reference in references)
        {
            var lot = reference.Lots.Find(x => x.LotStatus == ELotStatus.Working);

            var viewModel = new ParameterViewModel(
                reference.DeviceType,
                reference.Product.ProductName,
                reference.RefName,
                lot is null ? string.Empty : lot.LotId,
                lot is null ? 0 : lot.LotSize,
                await MapToDeviceInfoViewModel(reference));

            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    private async Task<List<DeviceInfoViewModel>> MapToDeviceInfoViewModel(Reference reference)
    {
        var viewModels = new List<DeviceInfoViewModel>();

        var deviceReferences = await _context.DeviceReferences
                .Include(x => x.Device)
                .ThenInclude(x => x.WorkRecords)
                .ThenInclude(x => x.Person)
                .Include(x => x.Reference)
                .Include(x => x.MFCs)
                .Where(x => x.ReferenceId == reference.Id)
                .ToListAsync();

        foreach (var deviceReference in deviceReferences)
        {
            var persons = new List<Person>();

            var workRecords = deviceReference.Device.WorkRecords.Where(x => x.WorkStatus == EWorkStatus.Working).ToList();
            workRecords.ForEach(x => persons.Add(x.Person));

            var viewModel = new DeviceInfoViewModel(
                deviceReference.DeviceId,
                _mapper.Map<List<PersonWorkingViewModel>>(persons),
                _mapper.Map<List<MFCViewModel>>(deviceReference.MFCs));

            viewModels.Add(viewModel);
        }
        
        return viewModels;
    }
}
