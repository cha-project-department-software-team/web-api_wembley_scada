using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Lot
{
    public int Id { get; set; }
    public string LotId { get; set; }
    public int LotSize { get; set; }
    public ELotStatus LotStatus { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Lot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Lot(string lotId, int lotSize, ELotStatus lotStatus, DateTime startTime)
    {
        LotId = lotId;
        LotSize = lotSize;
        LotStatus = lotStatus;
        StartTime = startTime;
    }

    public void Update(string lotId, int lotSize, ELotStatus? lotStatus, DateTime? endTime)
    {
        LotId = lotId;
        LotSize = lotSize;
        if (lotStatus is not null)
        {
            LotStatus = (ELotStatus)lotStatus;
        }
        EndTime = endTime;
    }
}
