using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record LogOutUserRequest : IRequest<LogOutUserResponse>
{
    public required int UserId { get; init; }
}