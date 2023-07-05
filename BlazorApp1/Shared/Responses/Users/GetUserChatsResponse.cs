using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record GetUserChatsResponse
{
    public required ChatroomModel[] Chats { get; init; }
}