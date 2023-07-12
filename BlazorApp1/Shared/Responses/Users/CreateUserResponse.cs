using System.Security.Claims;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record CreateUserResponse
{
    public required ClaimsPrincipal Principal { get; init; }
    public required UserModel User { get; init; }
}