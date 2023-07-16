using AutoMapper;
using BlazorApp1.Server.Exceptions;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class AuthorizeUserHandler : IRequestHandler<AuthorizeUserRequest, AuthorizeUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthorizeUserHandler(IUserService userService, IAuthService authService, IMapper mapper)
    {
        _userService = userService;
        _authService = authService;
        _mapper = mapper;
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
            return new AuthorizeUserResponse { User = principal, AuthorizedUser = _mapper.Map<UserModel>(user)};
        }

        throw new NotFoundException("Неверный логин или пароль");
    }
}