using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Server.Handlers.Users;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserService userService, IMapper mapper, IAuthService authService)
    {
        _userService = userService;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var entity = await _userService.CreateUser(request.Name, request.Password);
        var model = _mapper.Map<UserModel>(entity);
        var principal = await _authService.AuthUser(entity);
        return new CreateUserResponse { User = model, Principal = principal };
    }
}