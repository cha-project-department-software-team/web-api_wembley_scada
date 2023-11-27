using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Services;
using WembleyScadaThaiDuongScada.Host.Application.Commands;

namespace WembleyScada.Host.Application.Commands;
public class DefectCountNotificationHandler : INotificationHandler<DefectCountNotification>
{
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public DefectCountNotificationHandler(IShiftReportRepository shiftReportRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _shiftReportRepository = shiftReportRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(DefectCountNotification notification, CancellationToken cancellationToken)
    {
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(notification.Timestamp);
        var shiftDate = ShiftTimeHelper.GetShiftDate(notification.Timestamp);
        var shiftReport = await _shiftReportRepository.GetAsync(notification.DeviceId, shiftNumber, shiftDate);
        if (shiftReport is null)
        {
            return;
        }

        shiftReport.SetDefectCount(notification.DefectCount);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        if (notification.DefectCount > 0)
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "Q", shiftReport.Q, notification.Timestamp);
            await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "OEE", shiftReport.OEE, notification.Timestamp);
        }     
    }
}
