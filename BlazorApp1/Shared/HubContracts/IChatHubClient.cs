using BlazorApp1.Shared.Responses.Chats;

namespace BlazorApp1.Shared.HubContracts;

public interface IChatHubClient
{
    public Task MessageSent(SendMessageResponse response);
}