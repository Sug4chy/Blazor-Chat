using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class SendMessageHandler : IRequestHandler<SendMessageRequest, SendMessageResponse>
{
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public SendMessageHandler(IUserService userService, IMessageService messageService, IMapper mapper)
    {
        _userService = userService;
        _messageService = messageService;
        _mapper = mapper;
    }

    public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
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