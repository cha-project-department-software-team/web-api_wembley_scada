using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Buffers;
using WembleyScada.Host.Application.Services;
using WembleyScada.Infrastructure.Communication;
using Buffer = WembleyScada.Api.Application.Workers.Buffer;

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
    private readonly Buffer _buffer;

    public HerapinCapMachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers, MetricMessagePublisher metricMessagePublisher, ManagedMqttClient mqttClient, IShiftReportRepository shiftReportRepository, IMachineStatusRepository machineStatusRepository, IDeviceRepository deviceRepository, IErrorInformationRepository errorInformationRepository, Buffer buffer)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _metricMessagePublisher = metricMessagePublisher;
        _mqttClient = mqttClient;
        _shiftReportRepository = shiftReportRepository;
        _machineStatusRepository = machineStatusRepository;
        _deviceRepository = deviceRepository;
        _errorInformationRepository = errorInformationRepository;
        _buffer = buffer;
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
            _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
        }
        else
        {
            HandleErrorStatus(notification, latestStatus);
        }

        if (notification.MachineStatus == EMachineStatus.Off)
        {
            await HandleOffStatus(notification);
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

            // In case still receive ProductCount while Machine Status is "error" and do not affect OEE in normal case
            _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
        }
    }

    private async Task HandleOffStatus(HerapinCapMachineStatusChangedNotification notification)
    {
        var tagChangedNotifications = _buffer.GetTagByDevice(notification.DeviceId);
        var tagIds = tagChangedNotifications.Select(x => x.TagId).ToList();
        
        foreach (var tagId in tagIds)
        {
            await _mqttClient.Publish($"{notification.DeviceType}/{notification.DeviceId}/Metric/{tagId}", string.Empty, true);
        }

        _buffer.ClearBufferByDevice(notification.DeviceId);

        var errorInformations = await _errorInformationRepository.GetByDeviceAsync(notification.DeviceId);
        var latestErrorStatues = errorInformations.Select(x =>
                x.ErrorStatuses.OrderByDescending(x => x.Timestamp)
                               .FirstOrDefault())
            .ToList();
        
        var errorStatues = latestErrorStatues.Where(x => x.Value == 1).ToList();
       
        foreach (var errorStatus in errorStatues)
        {
            if (errorStatus is null)
            {
                return;
            }
            await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "endErrorStatus", errorStatus.ErrorInformation.ErrorName, notification.Timestamp);
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
