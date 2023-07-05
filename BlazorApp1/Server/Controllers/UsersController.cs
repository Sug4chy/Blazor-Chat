using BlazorApp1.Server.Mappers;
using BlazorApp1.Server.Services;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserMapper _userMapper;
    private readonly ChatMapper _chatMapper;

    public UsersController(
        UserService userService, 
        UserMapper userMapper, 
        ChatMapper chatMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
        _chatMapper = chatMapper;
    }

    [HttpPost]
    public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
    {
        var entity = await _userService.CreateUser(request.Name);
        var model = _userMapper.Map(entity);
        return new CreateUserResponse { User = model };
    }
    
    [HttpGet]
    public async Task<GetAllUsersResponse> GetAllUsers([FromQuery] GetAllUsersRequest request)
    {
        var entities = await _userService.GetAllUsers();
        var models = entities.Select(_userMapper.Map)
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
            .Select(_chatMapper.Map)
            .ToArray();
        return new GetUserChatsResponse { UserName = user.Name, Chats = chats };
    }
}