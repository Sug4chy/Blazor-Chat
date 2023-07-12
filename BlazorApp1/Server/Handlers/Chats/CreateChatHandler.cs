using System.Security.Claims;
using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class CreateChatHandler : IRequestHandler<CreateChatRequest, CreateChatResponse>
{
    private readonly IUserService _userService;
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public CreateChatHandler(IUserService userService, IChatService chatService, IMapper mapper)
    {
        _userService = userService;
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<CreateChatResponse> Handle(CreateChatRequest request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(request.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {userId} не существует");
        }
        var entity = await _chatService.CreateChat(request.Name, user);
        var model = _mapper.Map<ChatModel>(entity);
        return new CreateChatResponse { Chat = model };
    }
}