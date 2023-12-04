using Microsoft.AspNetCore.Mvc;
using WembleyScada.Api.Application.Queries.ShiftReports;

namespace WembleyScada.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShiftReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<ShiftReportViewModel>> GetShiftReportsByTime([FromQuery]ShiftReportsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet]
    [Route("downloadReport")]
    public async Task<IActionResult> DownLoadExcelReport([FromQuery]DownloadReportsQuery query)
    {
        var file = await _mediator.Send(query);
        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OEEreport.xlsx");
    }

    [HttpGet]
    [Route("shiftReportId")]
    public async Task<IEnumerable<ShiftReportDetailViewModel>> GetShiftReportDetails([FromQuery]ShiftReportDetailsQuery query)
    {
        return await _mediator.Send(query);
    }
}
