using BlazorApp1.Server.Mappers;
using BlazorApp1.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserMapper _userMapper;
    private readonly ChatroomMapper _chatroomMapper;

    public UsersController(
        UserService userService, 
        UserMapper userMapper, 
        ChatroomMapper chatroomMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
        _chatroomMapper = chatroomMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(string name)
    {
        var entity = await _userService.CreateUser(name);
        var model = _userMapper.Map(entity);
        return Ok(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var entities = await _userService.GetAllUsers();
        var models = entities.Select(_userMapper.Map)
            .Select(user => user.Name)
            .ToList();
        return Ok(models);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUserChats(int userId)
    {
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            return BadRequest($"Пользователя с id {userId} не существует");
        }

        var chats = user.Chatrooms
            .Select(chat => chat.Name)
            .ToArray();
        return Ok(chats);
    }

}