using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record GetUserChatsResponse
{
    public required string UserName { get; init; }
    public required ChatModel[] Chats { get; init; }
}