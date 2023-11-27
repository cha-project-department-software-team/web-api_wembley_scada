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
    public TimeSpan ElapsedTime => ShiftTimeHelper.GetShiftElapsedTime(Date, ShiftNumber);
    public double A
    {
        get
        {
            double a = Shots.Count > 0 ? TimeSpan.FromSeconds(TotalCycleTime) / ElapsedTime : 0;
            a = (a > 1) ? 1 : a;
            return a;
        }
    }
    public double Q => Shots.Count > 0 ? (double)(ProductCount - DefectCount) / (double)ProductCount : 0;
    public double P => Shots.Count > 0 ? TotalExecutionTime / TotalCycleTime : 0;
    public double OEE => Shots.Count > 0 ? A * P * Q : 0;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ShiftReport() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ShiftReport(int shiftNumber, DateTime date, Device device)
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

    public void AddShot(double executionTime, double cycleTime, DateTime timestamp)
    {
        if (!Shots.Any(x => x.TimeStamp == timestamp))
        {
            var shot = new Shot(executionTime, cycleTime, timestamp);
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
}
