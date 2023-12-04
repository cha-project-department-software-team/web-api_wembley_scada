using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
using WembleyScada.Host.Application.Services;

namespace WembleyScada.Host.Application.Commands;

public class ErrorStatusNotificationHandler : INotificationHandler<ErrorStatusNotification>
{
    private readonly IErrorInformationRepository _errorInformationRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public ErrorStatusNotificationHandler(IErrorInformationRepository errorInformationRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _errorInformationRepository = errorInformationRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(ErrorStatusNotification notification, CancellationToken cancellationToken)
    {
        var errorInformation = await _errorInformationRepository.GetAsync(notification.ErrorId);
        if (errorInformation is null)
        {
            return;
        }

        errorInformation.AddErrorStatus(notification.Value, notification.Timestamp);

        await _errorInformationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        if (notification.Value == 1)
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "errorStatus", errorInformation.ErrorName, notification.Timestamp);
        }
        else
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.DeviceType, notification.DeviceId, "endErrorStatus", errorInformation.ErrorName, notification.Timestamp);
        }
    }
}
