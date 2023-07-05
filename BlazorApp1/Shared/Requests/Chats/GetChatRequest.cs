namespace BlazorApp1.Shared.Requests.Chats;

public record GetChatRequest
{
    public required int ChatId { get; init; }
}