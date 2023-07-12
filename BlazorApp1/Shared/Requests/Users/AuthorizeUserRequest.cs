using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record AuthorizeUserRequest : IRequest<AuthorizeUserResponse>
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}