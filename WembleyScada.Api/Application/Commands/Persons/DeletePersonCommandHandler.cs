using WembleyScada.Domain.AggregateModels.PersonAggregate;

namespace WembleyScada.Api.Application.Commands.Persons;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _personRepository.DeleteAsync(request.PersonId);
        return await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
