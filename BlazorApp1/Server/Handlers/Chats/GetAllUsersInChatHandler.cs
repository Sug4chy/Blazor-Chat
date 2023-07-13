using AutoMapper;
using BlazorApp1.Server.Exceptions;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class GetAllUsersInChatHandler : IRequestHandler<GetAllUsersInChatRequest, GetAllUsersInChatResponse>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public GetAllUsersInChatHandler(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<GetAllUsersInChatResponse> Handle(GetAllUsersInChatRequest request, CancellationToken cancellationToken)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        NotFoundException.ThrowIfNull(chat);

        var users = chat.Users.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersInChatResponse { Chat = _mapper.Map<ChatModel>(chat), UsersInChat = users };
    }
}