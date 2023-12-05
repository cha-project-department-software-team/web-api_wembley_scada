using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;

    public UpdateLotCommandHandler(IReferenceRepository referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }

    public async Task<bool> Handle(UpdateLotCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.RefName) ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.RefName}' cannot be found.");

        reference.UpdateLot(request.LotId, request.LotSize, DateTime.UtcNow.AddHours(7));

       return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
