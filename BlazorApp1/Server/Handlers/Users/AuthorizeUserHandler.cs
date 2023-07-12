using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class AuthorizeUserHandler : IRequestHandler<AuthorizeUserRequest, AuthorizeUserResponse>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthorizeUserHandler(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    public async Task<AuthorizeUserResponse> Handle(AuthorizeUserRequest request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsers();
        var hashPassword = await _authService.HashPassword(request.Password);
        foreach (var user in users)
        {
            if (!request.UserName.Equals(user.Name) || !hashPassword.Equals(user.HashPassword))
            {
                continue;
            }

            var principal = await _authService.AuthUser(user);
            return new AuthorizeUserResponse { User = principal };
        }

        throw new ArgumentException($"Неверный логин или пароль");
    }
}