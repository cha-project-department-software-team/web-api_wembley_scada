using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.MachineStatus;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MachineStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public MachineStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<MachineStatusViewModel>> GetListAsync([FromQuery] MachineStatusQuery query)
    {
        return await _mediator.Send(query);
    }
}