using System.Security.Claims;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record DeleteUserRequest : IRequest<DeleteUserResponse>
{
    public ClaimsPrincipal? User { get; init; }
    public int UserId { get; init; }
}