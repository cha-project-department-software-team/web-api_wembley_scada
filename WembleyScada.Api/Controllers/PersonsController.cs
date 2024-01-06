using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Commands.Persons;
using WembleyScada.Api.Application.Queries.Persons;

namespace WembleyScada.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand command)
    {
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return NotFound(errorMessage);
        }
    }

    [HttpGet]
    public async Task<IEnumerable<PersonViewModel>> GetPersons([FromQuery] PersonsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete]
    [Route("{personId}")]
    public async Task<IActionResult> DeletePerson([FromRoute] string personId)
    {
        var command = new DeletePersonCommand(personId);
        try
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return NotFound(errorMessage);
        }
    }

    [HttpPost]
    [Route("PersonWorkRecords/{deviceId}")]
    public async Task<IActionResult> CreatePersonWorkRecord([FromRoute] string deviceId, [FromBody] CreatePersonWorkRecordViewModel personWorkRecord)
    {
        var command = new CreatePersonWorkRecordCommand(deviceId, personWorkRecord.PersonIds);
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
