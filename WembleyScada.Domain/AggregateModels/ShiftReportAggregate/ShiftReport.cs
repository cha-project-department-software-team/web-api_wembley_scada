using WembleyScada.Domain.AggregateModels.DeviceAggregate;

namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
public class ShiftReport : IAggregateRoot
{
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public Device Device { get; set; }
    public DateTime Date { get; set; }
    public int ShiftNumber { get; set; }
    public List<Shot> Shots { get; set; }
    public int ProductCount { get; set; }
    public int DefectCount { get; set; }
    public double TotalExecutionTime => Shots.Sum(x => x.ExecutionTime);
    public double TotalCycleTime => Shots.Sum(x => x.CycleTime);
    public TimeSpan ElapsedTime { get; set; }
    public double A { get; set; }
    public double P { get; set; }
    public double Q => Shots.Count > 0 ? (double)(ProductCount - DefectCount) / (double)ProductCount : 0;
    public double OEE => Shots.Count > 0 ? A * P * Q : 0;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ShiftReport() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ShiftReport(Device device, int shiftNumber, DateTime date)
    {
        ShiftNumber = shiftNumber;
        Date = date;
        Device = device;
        DeviceId = device.DeviceId;
        Shots = new List<Shot>();
    }

    public ShiftReport(Device device, DateTime time)
    {
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(time);
        var date = ShiftTimeHelper.GetShiftDate(time);
        ShiftNumber = shiftNumber;
        Date = date;
        Device = device;
        DeviceId = device.DeviceId;
        Shots = new List<Shot>();
    }

    public void AddHerapinCapShot(DateTime timestamp, double a, double p, double q, double oEE)
    {
        if (!Shots.Any(x => x.TimeStamp == timestamp))
        {
            var shot = new Shot(timestamp, a, p, q, oEE);
            Shots.Add(shot);
        }
    }

    public void AddShot(double executionTime, double cycleTime, DateTime timestamp, double a, double p, double q, double oEE)
    {
        if (!Shots.Any(x => x.TimeStamp == timestamp))
        {
            var shot = new Shot(executionTime, cycleTime, timestamp, a, p, q, oEE);
            Shots.Add(shot);
        }
    }

    public void SetProductCount(int productCount)
    {
        ProductCount = productCount;
    }

    public void SetDefectCount(int defectCount)
    {
        DefectCount = defectCount;
    }

    public void SetElapsedTime(TimeSpan elapsedTime)
    {
        ElapsedTime = elapsedTime;
    }

    public void SetA(double a)
    {
        A = a;
    }

    public void SetP(double p)
    { 
        P = p; 
    }
}
