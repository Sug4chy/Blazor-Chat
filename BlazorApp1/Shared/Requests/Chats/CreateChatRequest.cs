namespace BlazorApp1.Shared.Requests.Chats;

public record CreateChatRequest
{
    public required string Name { get; init; }
}