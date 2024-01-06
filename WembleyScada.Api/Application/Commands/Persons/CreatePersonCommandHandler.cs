using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person(request.PersonId, request.PersonName);
        
        await _personRepository.AddAsync(person);

        return await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
