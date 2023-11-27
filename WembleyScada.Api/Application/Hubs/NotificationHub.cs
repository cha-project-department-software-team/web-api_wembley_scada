using Microsoft.AspNetCore.SignalR;
using Buffer = WembleyScada.Api.Application.Workers.Buffer;

namespace WembleyScada.Api.Application.Hubs;

public class NotificationHub: Hub
{
    private readonly Buffer _buffer;

    public NotificationHub(Buffer buffer)
    {
        _buffer = buffer;
    }

    public string SendAll()
    {
        string allTags = _buffer.GetAllTag();
        return allTags;
    }

    public async Task SendAllTag()
    {
        string allTags = _buffer.GetAllTag();

        await Clients.All.SendAsync("GetAll", allTags);
    }
}
