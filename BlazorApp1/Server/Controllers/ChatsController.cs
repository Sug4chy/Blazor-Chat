using BlazorApp1.Server.Mappers;
using BlazorApp1.Server.Services;
using BlazorApp1.Shared.Requests;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses;
using BlazorApp1.Shared.Responses.Chats;
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
        ChatroomMapper chatroomMapper, 
        ChatService chatService, 
        MessageService messageService, 
        MessageMapper messageMapper, 
        UserMapper userMapper)
    {
        _userService = userService;
        _chatroomMapper = chatroomMapper;
        _chatService = chatService;
        _messageService = messageService;
        _messageMapper = messageMapper;
        _userMapper = userMapper;
    }
    
    [HttpPost]
    public async Task<CreateChatResponse> CreateChat([FromBody] CreateChatRequest request)
    {
        var entity = await _chatService.CreateChat(request.Name);
        var model = _chatroomMapper.Map(entity);
        return new CreateChatResponse { Chat = model };
    }
    
    [HttpGet("{chatId:int}")]
    public async Task<GetChatResponse> GetChat([FromRoute, FromBody] GetChatRequest request)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чата с id {request.ChatId} не существует");
        }

        var chatModel = _chatroomMapper.Map(chat);
        return new GetChatResponse { Chat = chatModel };
    }

    [HttpPut("{chatId:int}/users")]
    public async Task<AddUserInChatResponse> AddUserInChat([FromBody] AddUserInChatRequest request)
    {
        var user = await _userService.GetUser(request.UserId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Пользователя с id {request.ChatId} не существует");
        }

        try
        {
            await _chatService.AddUserInChat(user, chat);
        }
        catch(DbUpdateException)
        {
            //Ignore
        }

        return new AddUserInChatResponse { UpdatedChat = _chatroomMapper.Map(chat) };
    }

    [HttpGet("{chatId:int})/users")]
    public async Task<GetAllUsersInChatResponse> GetAllUsersInChat([FromRoute, FromBody] GetAllUsersInChatRequest request)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чат с id {request.ChatId} не существует");
        }

        var users = chat.Users.Select(_userMapper.Map)
            .ToArray();
        return new GetAllUsersInChatResponse { Chat = _chatroomMapper.Map(chat), UsersInChat = users };
    }
    
    [HttpPost("{chatId:int}")]
    public async Task<SendMessageResponse> SendMessage([FromBody] SendMessageRequest request)
    {
        var user = await _userService.GetUser(request.UserId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        var chat = user.Chatrooms.FirstOrDefault(chat => chat.Id == request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Пользователь {user.Name} не состоит в чате {request.ChatId}");
        }

        var message = await _messageService.CreateMessage(request.Text, user, chat);
        var messageModel = _messageMapper.Map(message);
        return new SendMessageResponse { Message = messageModel };
    }
}