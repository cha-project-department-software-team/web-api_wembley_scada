using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Commands.DeviceReferences;
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
    public async Task<IEnumerable<DeviceReferenceViewModel>> GetDeviceReferences([FromQuery] DeviceReferencesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPut]
    [Route("{deviceId}/{referenceId}")]
    public async Task<IActionResult> UpdateMFCs([FromRoute]string deviceId, [FromRoute] int referenceId, [FromBody] List<UpdateMFCViewModel> mfcs)
    {
        var command = new UpdateDeviceReferenceCommand(referenceId, deviceId, mfcs);
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (ResourceNotFoundException ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return NotFound(errorMessage);
        }
    }
}
