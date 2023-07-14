using AutoMapper;
using BlazorApp1.Server.Exceptions;
using BlazorApp1.Server.Hubs;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.HubContracts;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1.Server.Handlers.Chats;

public class SendMessageHandler : IRequestHandler<SendMessageRequest, SendMessageResponse>
{
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;
    private readonly IHubContext<ChatHub> _hubContext; 

    public SendMessageHandler(IUserService userService, IMessageService messageService, IMapper mapper, IHubContext<ChatHub> hubContext)
    {
        _userService = userService;
        _messageService = messageService;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(request.UserId);
        NotFoundException.ThrowIfNull(user);

        var chat = user.Chatrooms.FirstOrDefault(chat => chat.Id == request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Пользователь {user.Name} не состоит в чате {request.ChatId}");
        }

        var message = await _messageService.CreateMessage(request.Text, user, chat);
        var messageModel = _mapper.Map<MessageModel>(message);
        var groupName = $"Chat_{request.ChatId}";
        var response = new SendMessageResponse { Message = messageModel };
        await _hubContext.Clients.Group(groupName).SendCoreAsync(nameof(IChatHubClient.MessageSent),
            new object?[] { response }, cancellationToken);
        return response;
    }
}