using System.Security.Claims;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record LogOutUserRequest : IRequest<LogOutUserResponse>
{
    public ClaimsPrincipal? User { get; init; }
}