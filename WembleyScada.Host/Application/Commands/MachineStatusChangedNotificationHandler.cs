using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Infrastructure.Communication;

namespace WembleyScada.Host.Application.Commands;
public class MachineStatusChangedNotificationHandler : INotificationHandler<MachineStatusChangedNotification>
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMachineStatusRepository _machineStatusRepository;

    public MachineStatusChangedNotificationHandler(ManagedMqttClient mqttClient, IDeviceRepository deviceRepository, IMachineStatusRepository machineStatusRepository)
    {
        _mqttClient = mqttClient;
        _deviceRepository = deviceRepository;
        _machineStatusRepository = machineStatusRepository;
    }

    public async Task Handle(MachineStatusChangedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.MachineStatus == EMachineStatus.Off)
        {
            await ClearDataDevices(notification.DeviceType, notification.DeviceId, notification.Timestamp);
        }

        var device = await _deviceRepository.GetAsync(notification.DeviceId);
        if (device is null)
        {
            return;
        }

        var machineStatus = new MachineStatus(device, notification.MachineStatus, notification.Timestamp);
        if (!await _machineStatusRepository.ExistsAsync(notification.DeviceId, notification.Timestamp))
        {
            var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.DeviceId);
            
            if (latestStatus is null || notification.MachineStatus != latestStatus.Status)
            {
                await _machineStatusRepository.AddAsync(machineStatus);
            }
        }
        await _machineStatusRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task ClearDataDevices(string deviceType, string deviceId, DateTime timestamp)
    {
        await _mqttClient.Publish($"{deviceType}/{deviceId}/Metric/cycleTime", "", true);
        await _mqttClient.Publish($"{deviceType}/{deviceId}/Metric/executionTime", "", true);
        await _mqttClient.Publish($"{deviceType}/{deviceId}/Metric/badProduct", "", true);
        await _mqttClient.Publish($"{deviceType}/{deviceId}/Metric/operationTime", "", true);
        await _mqttClient.Publish($"{deviceType}/{deviceId}/Metric/counterShot", "", true);
    }
}
