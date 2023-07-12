using AutoMapper;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Server.Handlers.Chats;

public class GetAllChatsHandler : IRequestHandler<GetAllChatsRequest, GetAllChatsResponse>
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public GetAllChatsHandler(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<GetAllChatsResponse> Handle(GetAllChatsRequest request, CancellationToken cancellationToken)
    {
        var chats = await _chatService.GetAllChats();
        return new GetAllChatsResponse { AllChats = chats.Select(_mapper.Map<ChatModel>).ToArray() };
    }
}