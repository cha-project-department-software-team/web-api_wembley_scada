namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
public class Shot
{
    public double ExecutionTime { get; private set; }
    public double CycleTime { get; private set; }
    public DateTime TimeStamp { get; private set; }

    public Shot(double executionTime, double cycleTime, DateTime timeStamp)
    {
        ExecutionTime = executionTime;
        CycleTime = cycleTime;
        TimeStamp = timeStamp;
    }
}
