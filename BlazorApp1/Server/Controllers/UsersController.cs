using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(
        IUserService userService, 
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
    {
        var entity = await _userService.CreateUser(request.Name);
        var model = _mapper.Map<UserModel>(entity);
        return new CreateUserResponse { User = model };
    }
    
    [HttpGet]
    public async Task<GetAllUsersResponse> GetAllUsers([FromQuery] GetAllUsersRequest request)
    {
        var entities = await _userService.GetAllUsers();
        var models = entities.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersResponse { AllUsers = models };
    }

    [HttpGet("{userId:int}")]
    public async Task<GetUserChatsResponse> GetUserChats([FromRoute, FromBody] GetUserChatsRequest request)
    {
        var user = await _userService.GetUser(request.UserId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        var chats = user.Chatrooms
            .Select(_mapper.Map<ChatModel>)
            .ToArray();
        return new GetUserChatsResponse { UserName = user.Name, Chats = chats };
    }
}