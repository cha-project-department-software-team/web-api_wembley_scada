namespace WembleyScada.Host.Application.Dtos;
public class MessageType
{
    public EMessageType Value { get; }

    public MessageType(string deviceType, MetricMessage message)
    {
        if (deviceType == "HCM" && message.Name == "productCount")
        {
            Value = EMessageType.HerapinCapProductCount;
        }
        else if (deviceType == "HCM" && message.Name == "machineStatus")
        {
            Value = EMessageType.HerapinCapMachineStatus;
        }
        else if (message.Name == "cycleTime")
        {
            Value = EMessageType.CycleTime;
        }
        else if (message.Name == "executionTime")
        {
            Value = EMessageType.ExecutionTime;
        }
        else if (message.Name == "errorProduct")
        {
            Value = EMessageType.DefectsCount;
        }
        else if (message.Name.StartsWith("M"))
        {
            Value = EMessageType.ErrorStatus;
        }
        else
        {
            Value = EMessageType.Unspecified;
        }
    }

    public enum EMessageType
    {
        HerapinCapProductCount,
        HerapinCapMachineStatus,
        MachineStatus,
        CycleTime,
        ExecutionTime,
        DefectsCount,
        ErrorStatus,
        Unspecified
    }
}
