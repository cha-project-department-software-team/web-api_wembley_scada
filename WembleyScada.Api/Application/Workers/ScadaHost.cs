using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WembleyScada.Api.Application.Dtos;
using WembleyScada.Api.Application.Hubs;
using WembleyScada.Infrastructure.Communication;

namespace WembleyScada.Api.Application.Workers;

public class ScadaHost : BackgroundService
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly Buffer _buffer;
    private readonly IHubContext<NotificationHub> _hubContext;

    public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer, IHubContext<NotificationHub> hubContext)
    {
        _mqttClient = mqttClient;
        _buffer = buffer;
        _hubContext = hubContext;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectToMqttBrokerAsync();
    }

    private async Task ConnectToMqttBrokerAsync()
    {
        _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;
        await _mqttClient.ConnectAsync();

        await _mqttClient.Subscribe("IMM/+/Metric");
        await _mqttClient.Subscribe("IMM/+/Metric/+");
        await _mqttClient.Subscribe("HCM/+/Metric");
        await _mqttClient.Subscribe("HCM/+/Metric/+");
    }

    private async Task OnMqttClientMessageReceivedAsync(MqttMessage e)
    {
        var topic = e.Topic;
        var payloadMessage = e.Payload;
        if (topic is null || payloadMessage is null)
        {
            return;
        }

        var topicSegments = topic.Split('/');
        var deviceId = topicSegments[1];

        var metrics = JsonConvert.DeserializeObject<List<MetricMessage>>(payloadMessage);
        if (metrics is null)
        {
            return;
        }
        foreach (var metric in metrics)
        {
            var notification = new TagChangedNotification(deviceId, metric.Name, metric.Value, metric.Timestamp);
            _buffer.Update(notification);
            string json = JsonConvert.SerializeObject(notification);
            await _hubContext.Clients.All.SendAsync("TagChanged", json);
        }
    }
}
