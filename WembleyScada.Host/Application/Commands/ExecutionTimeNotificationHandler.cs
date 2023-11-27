using WembleyScada.Host.Application.Stores;

namespace WembleyScada.Host.Application.Commands;

public class ExecutionTimeNotificationHandler : INotificationHandler<ExecutionTimeNotification>
{
    private readonly ExecutionTimeBuffers _executionTimeBuffers;

    public ExecutionTimeNotificationHandler(ExecutionTimeBuffers executionTimeBuffers)
    {
        _executionTimeBuffers = executionTimeBuffers;
    }

    public Task Handle(ExecutionTimeNotification notification, CancellationToken cancellationToken)
    {
        _executionTimeBuffers.Update(notification.DeviceId, notification.ExecutionTime);
        return Task.CompletedTask;
    }
}
