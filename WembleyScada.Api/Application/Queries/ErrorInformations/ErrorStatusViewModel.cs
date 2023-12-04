using System.Runtime.CompilerServices;

namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusViewModel
{
    public string ErrorId { get; set; }
    public string ErrorName { get; set; }
    public DateTime Date { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ErrorStatusViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ErrorStatusViewModel(string errorId, string errorName, DateTime date, int shiftNumber, DateTime timestamp)
    {
        ErrorId = errorId;
        ErrorName = errorName;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }
}
