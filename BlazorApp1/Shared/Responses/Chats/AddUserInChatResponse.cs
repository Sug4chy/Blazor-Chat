using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Chats;

public record AddUserInChatResponse
{
    public required ChatModel UpdatedChat { get; init; }
}