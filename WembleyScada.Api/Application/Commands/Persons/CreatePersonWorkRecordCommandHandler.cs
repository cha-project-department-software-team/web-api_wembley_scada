using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

public class CreatePersonWorkRecordCommandHandler : IRequestHandler<CreatePersonWorkRecordCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IDeviceRepository _deviceRepository;

    public CreatePersonWorkRecordCommandHandler(IPersonRepository personRepository, IDeviceRepository deviceRepository)
    {
        _personRepository = personRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task<bool> Handle(CreatePersonWorkRecordCommand request, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.GetAsync(request.DeviceId) ?? throw new ResourceNotFoundException(nameof(Device), request.DeviceId);

        foreach (var personId in request.PersonIds)
        {
            var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);

            person.AddPersonWorkRecord(device, EWorkStatus.Working, DateTime.UtcNow.AddHours(7));
        }
        
        return await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
