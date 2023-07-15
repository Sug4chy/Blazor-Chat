using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class LogOutUserHandler : IRequestHandler<LogOutUserRequest, LogOutUserResponse>
{
    public async Task<LogOutUserResponse> Handle(LogOutUserRequest request, CancellationToken cancellationToken)
    {
        return new LogOutUserResponse();
    }
}