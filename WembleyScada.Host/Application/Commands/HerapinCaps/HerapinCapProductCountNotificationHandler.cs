using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Buffers;
using WembleyScada.Host.Application.Services;

namespace WembleyScada.Host.Application.Commands.HerapinCaps;

public class HerapinCapProductCountNotificationHandler : INotificationHandler<HerapinCapProductCountNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public HerapinCapProductCountNotificationHandler(StatusTimeBuffers statusTimeBuffers, IShiftReportRepository shiftReportRepository, IDeviceRepository deviceRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _shiftReportRepository = shiftReportRepository;
        _deviceRepository = deviceRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(HerapinCapProductCountNotification notification, CancellationToken cancellationToken)
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

        var startTime = _statusTimeBuffers.GetStartTime(notification.DeviceId);
        var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.DeviceId);
        var totalPreviousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.DeviceId);
        
        var runningTime = totalPreviousRunningTime + (notification.Timestamp - startRunningTime);
        var elapsedTime = notification.Timestamp - startTime;

        double A = runningTime / elapsedTime;
        double P = (3 * (notification.ProductCount / 4)) / (runningTime.TotalMilliseconds / 1000);
        
        shiftReport.SetA(A);
        shiftReport.SetP(P);
        shiftReport.SetElapsedTime(elapsedTime);
        shiftReport.SetProductCount(notification.ProductCount);
        shiftReport.AddHerapinCapShot(notification.Timestamp, shiftReport.A, shiftReport.P, shiftReport.Q, shiftReport.OEE);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "operationTime", shiftReport.ElapsedTime, notification.Timestamp);
    }
}
