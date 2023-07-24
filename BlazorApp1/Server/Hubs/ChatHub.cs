using System.Security.Claims;
using BlazorApp1.Server.Exceptions;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.HubContracts;
using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1.Server.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHubClient>, IChatHub
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public ChatHub(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }

    public Task<SendMessageResponse> SendMessage(SendMessageRequest request)
    {
        var userId = int.Parse(Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return _mediator.Send(request with { UserId = userId });
    }

    public override async Task OnConnectedAsync()
    {
        await JoinGroups();
        await base.OnConnectedAsync();
    }

    private async Task JoinGroups()
    {
        var userId = int.Parse(Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        NotFoundException.ThrowIfNull(user);

        var chatIds = user.Chatrooms.Select(chat => chat.Id);

        foreach (var chatId in chatIds)
        {
            var groupName = $"Chat_{chatId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}