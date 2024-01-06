using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

public class UpdateParameterCommandHandler : IRequestHandler<UpdateParameterCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IDeviceRepository _deviceRepository;

    public UpdateParameterCommandHandler(IReferenceRepository referenceRepository, IPersonRepository personRepository, IDeviceRepository deviceRepository)
    {
        _referenceRepository = referenceRepository;
        _personRepository = personRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task<bool> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.RefName) ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.RefName}' cannot be found.");
        reference.UpdateLotStatus(ELotStatus.Completed, DateTime.UtcNow.AddHours(7));

        var devices = await _deviceRepository.GetByTypeAsync(reference.DeviceType);
        foreach (var device in devices)
        {
            var workRecords = device.WorkRecords.Where(x => x.WorkStatus == EWorkStatus.Working).ToList();
            workRecords.ForEach(x => x.UpdateStatus(EWorkStatus.Completed, DateTime.UtcNow.AddHours(7)));
        }

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
