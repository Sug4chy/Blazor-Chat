using AutoMapper;
using BlazorApp1.Server.Exceptions;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Handlers.Chats;

public class AddUserInChatHandler : IRequestHandler<AddUserInChatRequest, AddUserInChatResponse>
{
    private readonly IUserService _userService;
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public AddUserInChatHandler(IUserService userService, IChatService chatService, IMapper mapper)
    {
        _userService = userService;
        _chatService = chatService;
        _mapper = mapper;
    }

    public async Task<AddUserInChatResponse> Handle(AddUserInChatRequest request, CancellationToken cancellationToken)
    {
        
        var user = await _userService.GetUser(request.UserId);
        NotFoundException.ThrowIfNull(user);

        var chat = await _chatService.GetChat(request.ChatId);
        NotFoundException.ThrowIfNull(chat);
        
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
}