using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.References;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReferencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReferencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<ReferenceViewModel>> GetReferences([FromQuery]ReferencesQuery query)
    {
        return await _mediator.Send(query);
    }
}
