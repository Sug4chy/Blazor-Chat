using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Chats;

public record GetAllUsersInChatResponse
{
    public required ChatModel Chat { get; init; }
    public required UserModel[] UsersInChat { get; init; }
}