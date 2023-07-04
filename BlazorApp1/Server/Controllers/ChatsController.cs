using BlazorApp1.Server.Mappers;
using BlazorApp1.Server.Services;
using BlazorApp1.Shared.Requests;
using BlazorApp1.Shared.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserMapper _userMapper;
    private readonly ChatService _chatService;
    private readonly ChatroomMapper _chatroomMapper;
    private readonly MessageService _messageService;
    private readonly MessageMapper _messageMapper;

    public ChatsController(
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
    public async Task<CreateChatResponse> CreateChat([FromBody] CreateChatRequest request)
    {
        var entity = await _chatService.CreateChat(request.Name);
        var model = _chatroomMapper.Map(entity);
        return new CreateChatResponse { Chat = model };
    }
    
    [HttpGet("{chatId:int}")]
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

    [HttpPut("{chatId:int}/users")]
    public async Task<IActionResult> AddUserInChat(int userId, int chatId)
    {
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            return BadRequest($"Пользователя с id {userId} не существует");
        }

        var chat = await _chatService.GetChat(chatId);
        if (chat is null)
        {
            return BadRequest($"Пользователя с id {chatId} не существует");
        }

        try
        {
            await _chatService.AddUserInChat(user, chat);
        }
        catch(DbUpdateException)
        {
            return Ok($"Пользователь {userId} уже есть в чате {chatId}");
        }

        return Ok();
    }

    [HttpPost("{chatId:int}/messages")]
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