using System.Security.Claims;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared;
using BlazorApp1.Shared.Requests.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp1.Server.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHubClient>, IChatHub
{
    private readonly IUserService _userService;

    public ChatHub(IUserService userService)
    {
        _userService = userService;
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        
    }

    public override async Task OnConnectedAsync()
    {
        await JoinGroups();
        await base.OnConnectedAsync();
    }

    public async Task JoinGroups()
    {
        var userId = int.Parse(Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        var chatIds = user!.Chatrooms.Select(chat => chat.Id);
        foreach (var chatId in chatIds)
        {
            var groupName = $"Chat_{chatId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}