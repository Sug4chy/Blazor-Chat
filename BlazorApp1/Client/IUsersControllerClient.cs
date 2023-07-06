using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Refit;

namespace BlazorApp1.Client;

public interface IUsersControllerClient
{
    [Post("/Users")]
    Task<CreateUserResponse> CreateUser(CreateUserRequest request);

    [Get("/Users")]
    Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request);

    [Get("/Users/{request.UserId}")]
    Task<GetUserChatsResponse> GetUserChats(GetUserChatsRequest request);
}