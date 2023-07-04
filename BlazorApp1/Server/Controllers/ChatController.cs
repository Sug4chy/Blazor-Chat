using BlazorApp1.Server.Mappers;
using BlazorApp1.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserMapper _userMapper;
    private readonly ChatService _chatService;
    private readonly ChatroomMapper _chatroomMapper;
    private readonly MessageService _messageService;
    private readonly MessageMapper _messageMapper;

    public ChatController(
        UserService userService, 
        UserMapper userMapper, 
        ChatroomMapper chatroomMapper, 
        ChatService chatService, 
        MessageService messageService, 
        MessageMapper messageMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
        _chatroomMapper = chatroomMapper;
        _chatService = chatService;
        _messageService = messageService;
        _messageMapper = messageMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(string name)
    {
        var entity = await _userService.CreateUser(name);
        var model = _userMapper.Map(entity);
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat(string name)
    {
        var entity = await _chatService.CreateChat(name);
        var model = _chatroomMapper.Map(entity);
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

    [HttpGet]
    public async Task<IActionResult> GetUsersChats(int userId)
    {
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            return BadRequest($"Пользователя с id {userId} не существует");
        }

        var chats = user.Chatrooms.Select(_chatroomMapper.Map)
            .Select(chat => chat.Name)
            .ToList();
        return Ok(chats);
    }

    [HttpGet]
    public async Task<IActionResult> GetChat(int chatId)
    {
        var chat = await _chatService.GetChat(chatId);
        if (chat is null)
        {
            return BadRequest($"Чата с id {chatId} не существует");
        }

        var chatModel = _chatroomMapper.Map(chat);
        return Ok(chatModel);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(int userId, int chatId, string text)
    {
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            return BadRequest($"Пользователя с id {userId} не существует");
        }

        var chat = user.Chatrooms.FirstOrDefault(chat => chat.Id == chatId);
        if (chat is null)
        {
            return BadRequest($"Пользователь {user.Name} не состоит в чате {chatId}");
        }

        var message = await _messageService.CreateMessage(text, user, chat);
        var messageModel = _messageMapper.Map(message);
        return Ok(messageModel);
    }
}