using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Chats;

public record SendMessageResponse
{
    public required MessageModel Message { get; init; }
}