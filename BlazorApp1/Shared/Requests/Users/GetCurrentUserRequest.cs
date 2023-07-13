using System.Security.Claims;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record GetCurrentUserRequest : IRequest<GetCurrentUserResponse>
{
    public ClaimsPrincipal? User { get; init; }
}