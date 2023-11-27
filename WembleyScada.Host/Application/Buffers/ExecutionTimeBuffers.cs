namespace WembleyScada.Host.Application.Stores;
public class ExecutionTimeBuffers 
{
    private readonly Dictionary<string, double> deviceLatestExecutionTimes = new();

    public void Update(string deviceId, double executionTime)
    {
        deviceLatestExecutionTimes[deviceId] = executionTime;
    }
    
    public double GetDeviceLatestExecutionTime(string deviceId)
    {
        return deviceLatestExecutionTimes[deviceId];
    }
}
