using System.Security.Claims;

namespace BlazorApp1.Shared.Responses.Users;

public record AuthorizeUserResponse
{
    public ClaimsPrincipal? User { get; init; }
}