using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Chats;

public record GetChatResponse
{
    public required ChatModel Chat { get; init; }
}