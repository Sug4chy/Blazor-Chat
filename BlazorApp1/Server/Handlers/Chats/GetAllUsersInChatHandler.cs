using AutoMapper;
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
        if (chat is null)
        {
            throw new ArgumentException($"Чат с id {request.ChatId} не существует");
        }

        var users = chat.Users.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersInChatResponse { Chat = _mapper.Map<ChatModel>(chat), UsersInChat = users };
    }
}