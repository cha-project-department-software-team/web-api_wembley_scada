using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Buffers;
using WembleyScada.Host.Application.Services;

namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapMachineStatusChangedNotificationHandler : INotificationHandler<HerapinCapMachineStatusChangedNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly MetricMessagePublisher _metricMessagePublisher;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IDeviceRepository _deviceRepository;

    public HerapinCapMachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers, MetricMessagePublisher metricMessagePublisher, IShiftReportRepository shiftReportRepository, IMachineStatusRepository machineStatusRepository, IDeviceRepository deviceRepository)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _metricMessagePublisher = metricMessagePublisher;
        _shiftReportRepository = shiftReportRepository;
        _machineStatusRepository = machineStatusRepository;
        _deviceRepository = deviceRepository;
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
            _statusTimeBuffers.UpdateStartTime(notification.DeviceId, notification.Timestamp);
            _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, TimeSpan.Zero);

            var latestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.DeviceId);

            var date = notification.Timestamp.Date;
            int shiftNumber = new();

            if (latestShiftReport is null || date != latestShiftReport.Date)
            {
                shiftNumber = 1;
            }
            else
            {
                shiftNumber = latestShiftReport.ShiftNumber + 1;
            }

            var shiftReport = new ShiftReport(device, shiftNumber, date);
            await _shiftReportRepository.AddAsync(shiftReport);
            await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        else if (notification.MachineStatus == EMachineStatus.Run)
        {
            _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
        }

        else
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run) 
            {
                var previousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.DeviceId);
                var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.DeviceId);
                var runningTime = notification.Timestamp - startRunningTime;
                _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.DeviceId, previousRunningTime + runningTime);

                //In case still receive ProductCount while Machine Status is "error" and do not effect to OEE in normal case
                _statusTimeBuffers.UpdateStartRunningTime(notification.DeviceId, notification.Timestamp);
            }
        }

        if (notification.MachineStatus == EMachineStatus.WifiDisconnted)
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Off)
            {
                await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "machineStatus", EMachineStatus.Off, notification.Timestamp);
                return;
            }
        }

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
