using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Chats;

public record GetAllChatsResponse
{
    public required ChatModel[] AllChats { get; init; }
}