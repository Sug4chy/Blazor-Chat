using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using Refit;

namespace BlazorApp1.Client;

public interface IChatsControllerClient
{
    [Get("/Chats/{request.ChatId}")]
    Task<GetChatResponse> GetChat(GetChatRequest request);

    [Post("/Chats/{request.ChatId}")]
    Task<SendMessageResponse> SendMessage(SendMessageRequest request);

    [Post("/Chats")]
    Task<CreateChatResponse> CreateChat(CreateChatRequest request);

    [Get("/Chats/{request.ChatId}/users")]
    Task<GetAllUsersInChatResponse> GetAllUsersInChat(GetAllUsersInChatRequest request);

    [Put("/Chats/{request.ChatId}/users")]
    Task<AddUserInChatResponse> AddUserInChat(AddUserInChatRequest request);
}