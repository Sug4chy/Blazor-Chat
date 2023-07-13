using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class GetChatHandler : IRequestHandler<GetChatRequest, GetChatResponse>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public GetChatHandler(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<GetChatResponse> Handle(GetChatRequest request, CancellationToken cancellationToken)
    {
        var chat = await _chatService.GetChat(request.ChatId);
        if (chat is null)
        {
            throw new ArgumentException($"Чата с id {request.ChatId} не существует");
        }

        var chatModel = _mapper.Map<ChatModel>(chat);
        return new GetChatResponse { Chat = chatModel };
    }
}