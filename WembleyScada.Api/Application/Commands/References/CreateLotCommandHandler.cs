using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;

    public CreateLotCommandHandler(IReferenceRepository referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }

    public async Task<bool> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.RefName) ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.RefName}' cannot be found.");

        reference.AddLot(request.LotId, request.LotSize);

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
