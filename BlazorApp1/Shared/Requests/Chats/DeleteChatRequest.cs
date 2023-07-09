namespace BlazorApp1.Shared.Requests.Chats;

public record DeleteChatRequest
{
    public required int ChatId { get; init; }
}