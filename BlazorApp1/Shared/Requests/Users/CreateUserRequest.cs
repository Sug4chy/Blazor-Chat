namespace BlazorApp1.Shared.Requests.Users;

public record CreateUserRequest
{
    public required string Name { get; init; }
}