namespace BlazorApp1.Shared.Requests.Chats;

public record GetAllUsersInChatRequest
{
    public required int ChatId { get; init; }
}