using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Lot
{
    public int Id { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Lot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Lot(string lotId, int lotSize, DateTime timestamp)
    {
        LotId = lotId;
        LotSize = lotSize;
        var date = ShiftTimeHelper.GetShiftDate(timestamp);
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }

    public void Update(string lotId, int lotSize, DateTime timestamp)
    {
        LotId = lotId;
        LotSize = lotSize;
        var date = ShiftTimeHelper.GetShiftDate(timestamp);
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = Timestamp;
    }
}
