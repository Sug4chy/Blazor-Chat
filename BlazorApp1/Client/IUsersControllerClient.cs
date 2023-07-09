using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Refit;

namespace BlazorApp1.Client;

public interface IUsersControllerClient
{
    [Post("/users")]
    Task<CreateUserResponse> CreateUser(CreateUserRequest request);

    [Get("/users")]
    Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request);

    [Get("/users/{request.UserId}")]
    Task<GetUserChatsResponse> GetUserChats(GetUserChatsRequest request);

    [Delete("/users")]
    Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request);
}