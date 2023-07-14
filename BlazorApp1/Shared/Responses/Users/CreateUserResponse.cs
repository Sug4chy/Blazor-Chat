using System.Security.Claims;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record CreateUserResponse
{
    public ClaimsPrincipal? User { get; init; }
    public required UserModel CreatedUser { get; init; }
}