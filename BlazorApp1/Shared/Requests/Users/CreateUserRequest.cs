namespace BlazorApp1.Shared.Requests;

public record CreateUserRequest
{
    public required string Name { get; init; }
}