using BlazorApp1.Shared.Requests.Chats;

namespace BlazorApp1.Server.Hubs;

public interface IChatHub
{
    public Task SendMessage(SendMessageRequest request);
}