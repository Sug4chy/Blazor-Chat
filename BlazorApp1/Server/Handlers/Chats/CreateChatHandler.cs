using System.Security.Claims;
using AutoMapper;
using BlazorApp1.Server.Exceptions;
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
        NotAllowedException.ThrowIfNull(user);

        var chat = await _chatService.CreateChat(request.Name, user);
        var chatModel = _mapper.Map<ChatModel>(chat);
        return new CreateChatResponse { Chat = chatModel };
    }
}