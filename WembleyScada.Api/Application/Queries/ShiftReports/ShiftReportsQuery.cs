﻿namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportsQuery : PaginatedQuery, IRequest<IEnumerable<ShiftReportViewModel>>
{
    public string DeviceId { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}
