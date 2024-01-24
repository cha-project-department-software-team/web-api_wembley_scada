﻿namespace WembleyScada.Api.Application.Dtos;

public class TagChangedNotification
{
    public string DeviceType { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }
    public object TagValue { get; set; }
    public DateTime TimeStamp { get; set; }

    public TagChangedNotification(string deviceType, string deviceId, string tagId, object tagValue, DateTime timeStamp)
    {
        DeviceType = deviceType;
        DeviceId = deviceId;
        TagId = tagId;
        TagValue = tagValue;
        TimeStamp = timeStamp;
    }
}
