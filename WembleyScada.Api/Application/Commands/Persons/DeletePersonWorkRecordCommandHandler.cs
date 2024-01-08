using Azure.Core;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

public class DeletePersonWorkRecordCommandHandler : IRequestHandler<DeletePersonWorkRecordCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IDeviceRepository _deviceRepository;

    public DeletePersonWorkRecordCommandHandler(IPersonRepository personRepository, IDeviceRepository deviceRepository)
    {
        _personRepository = personRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task<bool> Handle(DeletePersonWorkRecordCommand request, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.GetAsync(request.DeviceId) ?? throw new ResourceNotFoundException(nameof(Device), request.DeviceId);

        foreach (var personId in request.PersonIds)
        {
           device.DeleteWorkRecord(personId);
        }

        return await _deviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
