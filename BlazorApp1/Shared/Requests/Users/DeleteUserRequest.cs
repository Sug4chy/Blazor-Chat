namespace BlazorApp1.Shared.Requests.Users;

public record DeleteUserRequest
{
    public int UserId { get; init; }
}