using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Buffers;
using WembleyScada.Host.Application.Services;
using WembleyScada.Infrastructure.Communication;

namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapMachineStatusChangedNotificationHandler : INotificationHandler<HerapinCapMachineStatusChangedNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly MetricMessagePublisher _metricMessagePublisher;
    private readonly ManagedMqttClient _mqttClient;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IErrorInformationRepository _errorInformationRepository;  

    public HerapinCapMachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers, MetricMessagePublisher metricMessagePublisher, ManagedMqttClient mqttClient, IShiftReportRepository shiftReportRepository, IMachineStatusRepository machineStatusRepository, IDeviceRepository deviceRepository, IErrorInformationRepository errorInformationRepository)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _metricMessagePublisher = metricMessagePublisher;
        _mqttClient = mqttClient;
        _shiftReportRepository = shiftReportRepository;
        _machineStatusRepository = machineStatusRepository;
        _deviceRepository = deviceRepository;
        _errorInformationRepository = errorInformationRepository;
    }

    public async Task Handle(HerapinCapMachineStatusChangedNotification notification, CancellationToken cancellationToken)
    {
        var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.DeviceId);

        var device = await _deviceRepository.GetAsync(notification.DeviceId);
        if (device is null)
        {
            return;
        }

        if (notification.MachineStatus == EMachineStatus.On)
        {
            await HandleOnStatus(notification, device, cancellationToken);
        }

        else if (notification.MachineStatus == EMachineStatus.Run)
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run)
            {
                return;
            }
            _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
        }
        else
        {
            HandleErrorStatus(notification, latestStatus);
        }

        await UpdateMachineStatus(notification, device, latestStatus, cancellationToken);
    }

    private async Task HandleOnStatus(HerapinCapMachineStatusChangedNotification notification, Device device, CancellationToken cancellationToken)
    {
        _statusTimeBuffers.UpdateStartTime(notification.DeviceId, notification.Timestamp);
        _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, TimeSpan.Zero);

        var latestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.DeviceId);

        var date = notification.Timestamp.Date;
        int shiftNumber = (latestShiftReport is null || date != latestShiftReport.Date) ? 1 : latestShiftReport.ShiftNumber + 1;

        var shiftReport = new ShiftReport(device, shiftNumber, date);
        await _shiftReportRepository.AddAsync(shiftReport);
        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private void HandleErrorStatus(HerapinCapMachineStatusChangedNotification notification, MachineStatus? latestStatus)
    {
        if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run)
        {
            var previousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.DeviceId);
            var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.DeviceId);
            var runningTime = notification.Timestamp - startRunningTime;
            _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, previousRunningTime + runningTime);
        }
    }

    private async Task UpdateMachineStatus(HerapinCapMachineStatusChangedNotification notification, Device device, MachineStatus? latestStatus, CancellationToken cancellationToken)
    {
        var newestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.DeviceId);
        if (newestShiftReport is null)
        {
            return;
        }

        var machineStatus = new MachineStatus(device, notification.MachineStatus, newestShiftReport.ShiftNumber, newestShiftReport.Date, notification.Timestamp);

        if (notification.MachineStatus == EMachineStatus.Off)
        {
            machineStatus = new MachineStatus(device, notification.MachineStatus, newestShiftReport.ShiftNumber, newestShiftReport.Date, DateTime.UtcNow.AddHours(7));
        }

        if (!await _machineStatusRepository.ExistsAsync(notification.DeviceId, notification.Timestamp))
        {
            if (latestStatus is null || notification.MachineStatus != latestStatus.Status)
            {
                await _machineStatusRepository.AddAsync(machineStatus);
            }
        }
        await _machineStatusRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
