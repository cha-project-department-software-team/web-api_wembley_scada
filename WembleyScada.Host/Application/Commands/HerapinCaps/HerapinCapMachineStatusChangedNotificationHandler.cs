using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Host.Application.Buffers;
using WembleyScada.Infrastructure.Communication;

namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapMachineStatusChangedNotificationHandler : INotificationHandler<HerapinCapMachineStatusChangedNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly ManagedMqttClient _mqttClient;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IDeviceRepository _deviceRepository;

    public HerapinCapMachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers, ManagedMqttClient mqttClient, IMachineStatusRepository machineStatusRepository, IDeviceRepository deviceRepository)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _mqttClient = mqttClient;
        _machineStatusRepository = machineStatusRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task Handle(HerapinCapMachineStatusChangedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.MachineStatus == EMachineStatus.On)
        {
            _statusTimeBuffers.UpdateStartTime(notification.DeviceId, notification.Timestamp);
            _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, TimeSpan.Zero);
        }

        else if (notification.MachineStatus == EMachineStatus.Run)
        {
            _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
        }

        else
        {
            var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.DeviceId);
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run) 
            {
                var previousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.DeviceId);
                var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.DeviceId);
                var runningTime = notification.Timestamp - startRunningTime;
                _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, previousRunningTime + runningTime);
            }
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
}
