using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.ErrorInformations;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ErrorInformationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ErrorInformationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<ErrorStatusViewModel>> GetErrorStatuses([FromQuery]ErrorStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}
