using BlazorApp1.Shared.HubContracts;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp1.Client;

public class ChatHubClient : HubClientBase
{
    public event Action<SendMessageResponse> OnMessageSent = null!;

    public async Task SendMessage(SendMessageRequest request)
    {
        await Connection.InvokeCoreAsync(nameof(IChatHubClient.MessageSent), typeof(SendMessageResponse),
            new object?[] { request });
    }

    public ChatHubClient(NavigationManager navigationManager, IAccessTokenProvider accessTokenProvider) 
        : base(navigationManager, accessTokenProvider)
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Connection.On<SendMessageResponse>(
            nameof(IChatHubClient.MessageSent), 
            r => OnMessageSent.Invoke(r));
    }

    protected override string HubRelativeUrl => "/api/hubs/chats";
}