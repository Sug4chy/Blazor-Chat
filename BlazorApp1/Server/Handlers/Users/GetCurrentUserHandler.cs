using System.Security.Claims;
using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserRequest, GetCurrentUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetCurrentUserHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        if (request.User is NullUser)
        {
            return new GetCurrentUserResponse { CurrentUser = null };
        }

        var userId = int.Parse(request.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        return new GetCurrentUserResponse { CurrentUser = _mapper.Map<UserModel>(user) };
    }
}