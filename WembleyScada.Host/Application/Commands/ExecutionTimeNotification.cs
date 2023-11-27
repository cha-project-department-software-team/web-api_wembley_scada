namespace WembleyScada.Host.Application.Commands;
public class ExecutionTimeNotification: INotification
{
    public string DeviceId { get; set; }
    public double ExecutionTime { get; set; }

    public ExecutionTimeNotification(string deviceId, double executionTime)
    {
        DeviceId = deviceId;
        ExecutionTime = executionTime;
    }
}
