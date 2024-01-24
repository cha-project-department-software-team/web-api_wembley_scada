﻿using Newtonsoft.Json;
using WembleyScada.Domain.AggregateModels.MachineStatusAggregate;
using WembleyScada.Host.Application.Commands;
using WembleyScada.Host.Application.Commands.HerapinCaps;
using WembleyScada.Host.Application.Dtos;
using WembleyScada.Infrastructure.Communication;
using WembleyScadaThaiDuongScada.Host.Application.Commands;

namespace WembleyScada.Host.Application.Workers;
public class UpdateShiftReportWorker : BackgroundService
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly IServiceScopeFactory _scopeFactory;

    public UpdateShiftReportWorker(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory)
    {
        _mqttClient = mqttClient;
        _scopeFactory = scopeFactory;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectToMqttBrokerAsync();
    }

    private async Task ConnectToMqttBrokerAsync()
    {
        _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;
        await _mqttClient.ConnectAsync();

        await _mqttClient.Subscribe("HCM/+/Metric");
        await _mqttClient.Subscribe("HCM/+/Metric/+");
    }

    private async Task OnMqttClientMessageReceivedAsync(MqttMessage e)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        var topic = e.Topic;
        var payloadMessage = e.Payload;
        if (topic is null || payloadMessage is null)
        {
            return;
        }

        var topicSegments = topic.Split('/');
        var deviceType = topicSegments[0];
        var deviceId = topicSegments[1];

        var metrics = JsonConvert.DeserializeObject<List<MetricMessage>>(payloadMessage);
        if (metrics is null)
        {
            return;
        }

        foreach (var metric in metrics)
        {
            var messageType = new MessageType(deviceType, metric);
            switch (messageType.Value)
            {
                case MessageType.EMessageType.HerapinCapProductCount:
                    var productCount = Convert.ToInt32(metric.Value);
                    var herapinCapProductCount = new HerapinCapProductCountNotification(deviceType, deviceId, productCount, metric.Timestamp);
                    await mediator.Publish(herapinCapProductCount);
                    break;
                case MessageType.EMessageType.HerapinCapMachineStatus:
                    var statusCode = (long)metric.Value;
                    var machineStatus = (EMachineStatus)statusCode;
                    var herapinCapMachineStatus = new HerapinCapMachineStatusChangedNotification(deviceType, deviceId, machineStatus, metric.Timestamp);
                    await mediator.Publish(herapinCapMachineStatus);
                    break;
                case MessageType.EMessageType.CycleTime:
                    var cycleTime = (double)metric.Value;
                    var cycleTimeNotification = new CycleTimeNotification(deviceType, deviceId, cycleTime, metric.Timestamp);
                    await mediator.Publish(cycleTimeNotification);
                    break;
                case MessageType.EMessageType.ExecutionTime:
                    var executionTime = (double)metric.Value;
                    var executionTimeNotification = new ExecutionTimeNotification(deviceId, executionTime);
                    await mediator.Publish(executionTimeNotification);
                    break;
                case MessageType.EMessageType.DefectsCount:
                    var defectsCount = Convert.ToInt32(metric.Value);
                    var defectCountNotification = new DefectCountNotification(deviceType, deviceId, defectsCount, metric.Timestamp);
                    await mediator.Publish(defectCountNotification);
                    break;
                case MessageType.EMessageType.ErrorStatus:
                    var errorValue = Convert.ToInt32(metric.Value);
                    var errorStatusNotification = new ErrorStatusNotification(deviceType, deviceId, metric.Name, errorValue, metric.Timestamp);
                    await mediator.Publish(errorStatusNotification);
                    break;
            }
        }
    }
}
