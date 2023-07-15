using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record GetUserChatsResponse
{
    public string? UserName;
    public ChatModel[] UserChats { get; init; } = Array.Empty<ChatModel>();
}