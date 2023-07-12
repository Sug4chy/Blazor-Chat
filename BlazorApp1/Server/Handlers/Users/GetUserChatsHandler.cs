using System.Security.Claims;
using AutoMapper;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Server.Handlers.Users;

public class GetUserChatsHandler : IRequestHandler<GetUserChatsRequest, GetUserChatsResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserChatsHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetUserChatsResponse> Handle(GetUserChatsRequest request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(request.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {userId} не существует");
        }

        var chats = user.Chatrooms
            .Select(_mapper.Map<ChatModel>)
            .ToArray();
        return new GetUserChatsResponse { UserName = user.Name, Chats = chats };
    }
}