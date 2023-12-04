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
        shiftReport.SetProductCount(shiftReport.Shots.Count);
        shiftReport.AddShot(executionTime, notification.CycleTime, notification.Timestamp, shiftReport.A, shiftReport.P, shiftReport.Q, shiftReport.OEE);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType ,notification.DeviceId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "products", shiftReport.ProductCount, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "counterShot", shiftReport.Shots.Count, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "operationTime", shiftReport.TotalCycleTime, notification.Timestamp);
    }
}
