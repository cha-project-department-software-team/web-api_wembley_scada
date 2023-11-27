namespace WembleyScada.Host.Application.Dtos;
public class MessageType
{
    public EMessageType Value { get; }

    public MessageType(string deviceType, MetricMessage message)
    {
        if (message.Name == "machineStatus")
        {
            Value = EMessageType.MachineStatus;
        }
        else if (message.Name == "cycleTime")
        {
            Value = EMessageType.CycleTime;
        }
        else if (message.Name == "executionTime")
        {
            Value = EMessageType.ExecutionTime;
        }
        else if (message.Name == "badProduct")
        {
            Value = EMessageType.DefectsCount;
        }
        else
        {
            Value = EMessageType.Unspecified;
        }
    }

    public enum EMessageType
    {
        MachineStatus,
        CycleTime,
        ExecutionTime,
        DefectsCount,
        Unspecified
    }
}
