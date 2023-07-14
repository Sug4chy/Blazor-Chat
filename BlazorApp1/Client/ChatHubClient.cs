using BlazorApp1.Shared.HubContracts;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp1.Client;

public class ChatHubClient : HubClientBase
{
    public event Action<SendMessageResponse> OnMessageSent = null!;
    
    public ChatHubClient(NavigationManager navigationManager) 
        : base(navigationManager)
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Connection.On<SendMessageResponse>(
            nameof(IChatHubClient.MessageSent), 
            r => OnMessageSent.Invoke(r));
    }

    protected override string HubRelativeUrl => "/api/hubs/chat";

    public async Task SendMessage(SendMessageRequest request)
    {
        await Connection.InvokeCoreAsync(nameof(IChatHub.SendMessage), new object?[] { request });
    }
}