using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

public class UpdatePersonWorkRecordCommandHandler : IRequestHandler<UpdatePersonWorkRecordCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IDeviceRepository _deviceRepository;

    public UpdatePersonWorkRecordCommandHandler(IPersonRepository personRepository, IDeviceRepository deviceRepository)
    {
        _personRepository = personRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task<bool> Handle(UpdatePersonWorkRecordCommand request, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.GetAsync(request.DeviceId) ?? throw new ResourceNotFoundException(nameof(Device), request.DeviceId);
        var workRecords = device.WorkRecords.Where(x => x.WorkStatus == EWorkStatus.Working);

        var oldPersonIds = workRecords.Select(x => x.PersonId).ToList();
        
        foreach (var oldPersonId in oldPersonIds)
        { 
            var person = await _personRepository.GetAsync(oldPersonId) ?? throw new ResourceNotFoundException(nameof(Person), oldPersonId);
            person.DeleteWorkingRecord();
        }

        foreach (var newPersonId in request.PersonIds)
        {
            var person = await _personRepository.GetAsync(newPersonId) ?? throw new ResourceNotFoundException(nameof(Person), newPersonId);

            person.AddPersonWorkRecord(device, EWorkStatus.Working, DateTime.UtcNow.AddHours(7));
        }

        return await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
