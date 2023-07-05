using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses;

public record CreateUserResponse
{
    public required UserModel User { get; init; }
}