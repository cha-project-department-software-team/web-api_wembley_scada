using System.Text.Json;
using WembleyScada.Api.Application.Dtos;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Infrastructure.Communication;

namespace WembleyScada.Api.Application.Workers;

public class Buffer
{
    private readonly List<TagChangedNotification> tagChangedNotifications = new List<TagChangedNotification>();
    private readonly ManagedMqttClient _mqttClient;

    public Buffer(ManagedMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public async Task Update(TagChangedNotification notification)
    {
        if (notification.TagId != "errorStatus" && notification.TagId != "endErrorStatus")
        {
            var existedNotification = tagChangedNotifications.FirstOrDefault(n => n.DeviceId == notification.DeviceId && n.TagId == notification.TagId);

            if (existedNotification is null)
            {
                tagChangedNotifications.Add(notification);
            }
            else
            {
                existedNotification.TagValue = notification.TagValue;
            }
        }
            
        else if (notification.TagId == "errorStatus")
        {
            if (!tagChangedNotifications.Any(x => x.DeviceId == notification.DeviceId && x.TagId == "errorStatus" && (string)x.TagValue == (string)notification.TagValue))
            {
                tagChangedNotifications.Add(notification);
            }
        }

        else
        {
            var errorStatusExisted = tagChangedNotifications.Find(x => x.DeviceId == notification.DeviceId && x.TagId == "errorStatus" && (string)x.TagValue == (string)notification.TagValue);
            
            if (errorStatusExisted is not null)
            {
                tagChangedNotifications.Remove(errorStatusExisted);
            }
        }

        if (notification.TagId == "machineStatus")
        {
            var status = Convert.ToInt16(notification.TagValue);
            if (status == 5) //Off
            {
                var tagIds = tagChangedNotifications.Where(x => x.DeviceId == notification.DeviceId).Select(x => x.TagId).ToList();

                foreach (var tagId in tagIds)
                {
                    await _mqttClient.Publish($"{notification.DeviceType}/{notification.DeviceId}/Metric/{tagId}", "", true);
                }

                tagChangedNotifications.RemoveAll(x => x.DeviceId == notification.DeviceId);
            }
        }
    }

    public string GetAllTag() => JsonSerializer.Serialize(tagChangedNotifications);

    public List<TagChangedNotification> GetTagByDevice(string deviceId)
    {
        return tagChangedNotifications.Where(n => n.DeviceId == deviceId).ToList();
    }

    public void ClearBufferByDevice(string deviceId)
    {
        tagChangedNotifications.RemoveAll(n => n.DeviceId == deviceId);
    }
}
