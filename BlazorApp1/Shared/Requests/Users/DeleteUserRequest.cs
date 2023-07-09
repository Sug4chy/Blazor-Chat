namespace BlazorApp1.Shared.Requests.Users;

public record DeleteUserRequest
{
    public required int UserId { get; init; }
}