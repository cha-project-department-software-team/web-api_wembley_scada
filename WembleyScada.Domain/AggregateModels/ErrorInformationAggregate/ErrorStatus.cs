using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public class ErrorStatus
{
    public int Id { get; set; }
    public string ErrorId { get; set; }
    public ErrorInformation ErrorInformation { get; set; }
    public int Value { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ErrorStatus() { }

    public ErrorStatus(int value, DateTime timestamp)
    {
        Value = value;
        var date = ShiftTimeHelper.GetShiftDate(timestamp);
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }

    public ErrorStatus(int value, DateTime date, int shiftNumber, DateTime timestamp)
    {
        Value = value;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
