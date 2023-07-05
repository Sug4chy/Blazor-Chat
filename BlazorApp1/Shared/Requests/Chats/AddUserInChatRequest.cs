namespace BlazorApp1.Shared.Requests;

public record AddUserInChatRequest
{
    public required int UserId { get; init; }
    public required int ChatId { get; init; }
}