using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.DeviceReferences;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DeviceReferencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeviceReferencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<DeviceReferenceViewModel>> GetDeviceReferences([FromQuery]DeviceReferencesQuery query)
    {
        return await _mediator.Send(query);
    }
}
