using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Services;
using WembleyScada.Host.Application.Stores;

namespace WembleyScada.Host.Application.Commands;

public class CycleTimeNotificationHandler : INotificationHandler<CycleTimeNotification>
{
    private readonly ExecutionTimeBuffers _executionTimeBuffers;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public CycleTimeNotificationHandler(ExecutionTimeBuffers executionTimeBuffers, IShiftReportRepository shiftReportRepository, IDeviceRepository deviceRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _executionTimeBuffers = executionTimeBuffers;
        _shiftReportRepository = shiftReportRepository;
        _deviceRepository = deviceRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(CycleTimeNotification notification, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.GetAsync(notification.DeviceId);
        if (device is null)
        {
            return;
        }

        var shiftReport = new ShiftReport(device, notification.Timestamp);
        var existingShiftReport = await _shiftReportRepository.GetAsync(device.DeviceId, shiftReport.ShiftNumber, shiftReport.Date);
        if (existingShiftReport is not null)
        {
            shiftReport = existingShiftReport;
        }
        else
        {
            await _shiftReportRepository.AddAsync(shiftReport);
        }

        var executionTime = _executionTimeBuffers.GetDeviceLatestExecutionTime(notification.DeviceId);
        shiftReport.AddShot(executionTime, notification.CycleTime, notification.Timestamp);
        shiftReport.SetProductCount(shiftReport.Shots.Count);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType ,device.DeviceId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "products", shiftReport.ProductCount, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "counterShot", shiftReport.Shots.Count, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, device.DeviceId, "operationTime", shiftReport.TotalCycleTime, notification.Timestamp);
    }
}
