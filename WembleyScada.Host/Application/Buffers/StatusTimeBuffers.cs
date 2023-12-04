namespace WembleyScada.Host.Application.Buffers;
public class StatusTimeBuffers
{
    private readonly Dictionary<string, DateTime> startTimes = new();
    private readonly Dictionary<string, DateTime> startRunningTimes = new();
    private readonly Dictionary<string, TimeSpan> totalPreviousRunningTimes = new();

    public void UpdateStartTime(string deviceId, DateTime startTime)
    {
        startTimes[deviceId] = startTime;
    }

    public DateTime GetStartTime(string deviceId)
    {
        return startTimes[deviceId];
    }

    public void UpdateStartRunningTime(string deviceId, DateTime startRunningTime)
    {
        startRunningTimes[deviceId] = startRunningTime;
    }

    public DateTime GetStartRunningTime(string deviceId)
    {
        return startRunningTimes[deviceId];
    }

    public void UpdateTotalPreviousRunningTime(string deviceId, TimeSpan totalPreviousRunningTime)
    {
        totalPreviousRunningTimes[deviceId] = totalPreviousRunningTime;
    }

    public TimeSpan GetTotalPreviousRunningTime(string deviceId)
    {
        return totalPreviousRunningTimes[deviceId];
    }
}
