using BlazorApp1.Shared.Requests.Chats;
using BlazorApp1.Shared.Responses.Chats;
using Refit;

namespace BlazorApp1.Client;

public interface IChatsControllerClient
{
    [Get("/chats/{request.ChatId}")]
    Task<GetChatResponse> GetChat(GetChatRequest request);
    
    [Post("/chats")]
    Task<CreateChatResponse> CreateChat(CreateChatRequest request);
    
    [Delete("/chats")]
    Task<DeleteChatResponse> DeleteChat(DeleteChatRequest request);

    [Get("/chats/{request.ChatId}/users")]
    Task<GetAllUsersInChatResponse> GetAllUsersInChat(GetAllUsersInChatRequest request);

    [Put("/chats/{request.ChatId}/users")]
    Task<AddUserInChatResponse> AddUserInChat(AddUserInChatRequest request);
}