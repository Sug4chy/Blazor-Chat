using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ChatsController(
        IUserService userService,
        IChatService chatService, 
        IMessageService messageService, 
        IMapper mapper, 
        IMediator mediator)
    {
        _userService = userService;
        _chatService = chatService;
        _messageService = messageService;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public Task<CreateChatResponse> CreateChat([FromBody] CreateChatRequest request) => _mediator.Send(request);

    [HttpGet]
    [Authorize]
    public Task<GetAllChatsResponse> GetAllChats([FromQuery] GetAllChatsRequest request) => _mediator.Send(request);

    [HttpDelete]
    [Authorize]
    public async Task<DeleteChatResponse> DeleteChat([FromQuery] DeleteChatRequest request)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чата с id {request.ChatId} не существует");
        }

        await _chatService.DeleteChat(chat);
        return new DeleteChatResponse();
    }
    
    [HttpGet("{chatId:int}")]
    [Authorize]
    public async Task<GetChatResponse> GetChat([FromRoute, FromBody] GetChatRequest request)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чата с id {request.ChatId} не существует");
        }

        var chatModel = _mapper.Map<ChatModel>(chat);
        return new GetChatResponse { Chat = chatModel };
    }

    [HttpPut("{chatId:int}/users")]
    [Authorize]
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

        return new AddUserInChatResponse { UpdatedChat = _mapper.Map<ChatModel>(chat) };
    }

    [HttpGet("{ChatId:int}/users")]
    [Authorize]
    public async Task<GetAllUsersInChatResponse> GetAllUsersInChat([FromRoute, FromBody] GetAllUsersInChatRequest request)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чат с id {request.ChatId} не существует");
        }

        var users = chat.Users.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersInChatResponse { Chat = _mapper.Map<ChatModel>(chat), UsersInChat = users };
    }
    
    [HttpPost("{chatId:int}")]
    [Authorize]
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
        var messageModel = _mapper.Map<MessageModel>(message);
        return new SendMessageResponse { Message = messageModel };
    }
}