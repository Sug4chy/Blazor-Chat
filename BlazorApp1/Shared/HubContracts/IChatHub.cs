using BlazorApp1.Shared.Requests.Chats;

namespace BlazorApp1.Shared.HubContracts;

public interface IChatHub
{
    public Task SendMessage(SendMessageRequest request);
}