using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.Devices;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DevicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<DeviceViewModel>> GetDevices([FromQuery]DevicesQuery query)
    {
        return await _mediator.Send(query);
    }
}
