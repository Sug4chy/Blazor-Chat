using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var entities = await _userService.GetAllUsers();
        var models = entities.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersResponse { AllUsers = models };
    }
}