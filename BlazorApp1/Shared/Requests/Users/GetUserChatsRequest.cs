using System.Security.Claims;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record GetUserChatsRequest : IRequest<GetUserChatsResponse>
{
    public ClaimsPrincipal? User { get; init; }
    public required int UserId { get; init; }
}