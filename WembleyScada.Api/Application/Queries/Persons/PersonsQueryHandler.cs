using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WembleyScada.Infrastructure;

namespace WembleyScada.Api.Application.Queries.Persons;

public class PersonsQueryHandler : IRequestHandler<PersonsQuery, IEnumerable<PersonViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PersonsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PersonViewModel>> Handle(PersonsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Persons
            .Include(x => x.WorkRecords)
            .Include(x => x.WorkRecords)
            .ThenInclude(x => x.Device)
            .AsNoTracking();

        if (request.PersonId is not null)
        {
            queryable = queryable.Where(x => x.PersonId == request.PersonId);
        }

        var persons = await queryable.ToListAsync();
        return _mapper.Map<IEnumerable<PersonViewModel>>(persons);
    }
}
