using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;
    private readonly IDeviceReferenceRepository _deviceReferenceRepository;

    public CreateLotCommandHandler(IReferenceRepository referenceRepository, IDeviceReferenceRepository deviceReferenceRepository)
    {
        _referenceRepository = referenceRepository;
        _deviceReferenceRepository = deviceReferenceRepository;
    }

    public async Task<bool> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.RefName) ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.RefName}' cannot be found.");

        var referenceOfTypes = (await _referenceRepository.GetByTypeAsync(reference.DeviceType)).ToList();

        foreach (var referenceType in referenceOfTypes)
        {
            var workingLot = referenceType.Lots.Find(x => x.LotStatus == ELotStatus.Working);
            if (workingLot is not null)
            {
                throw new Exception($"This Device is working with Lot {workingLot.LotId}, you cannot create additional Lots");
            }
        }

        reference.AddLot(request.LotId, request.LotSize, ELotStatus.Working, DateTime.UtcNow.AddHours(7));

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
