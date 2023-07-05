namespace BlazorApp1.Shared.Requests.Users;

public record GetUserChatsRequest
{
    public required int UserId { get; init; }
}