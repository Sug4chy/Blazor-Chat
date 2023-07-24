using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;

namespace BlazorApp1.Shared.HubContracts;

public interface IChatHub
{
    public Task<SendMessageResponse> SendMessage(SendMessageRequest request);
}