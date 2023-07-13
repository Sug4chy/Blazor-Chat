using AutoMapper;
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
        var user = await _userService.GetUser(request.UserId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        var chats = user.Chatrooms
            .Select(_mapper.Map<ChatModel>)
            .ToArray();
        return new GetUserChatsResponse { UserName = user.Name, Chats = chats };
    }
}