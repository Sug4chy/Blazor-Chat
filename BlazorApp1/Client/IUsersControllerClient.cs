using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Refit;

namespace BlazorApp1.Client;

public interface IUsersControllerClient
{
    [Post("/users")]
    Task<CreateUserResponse> CreateUser(CreateUserRequest request);
    
    [Get("/Users/{request.UserId}")]
    Task<GetUserChatsResponse> GetUserChats(GetUserChatsRequest request);

    [Post("/users/auth")]
    Task<AuthorizeUserResponse> AuthorizeUser(AuthorizeUserRequest request);

    [Get("/users/current")]
    Task<GetCurrentUserResponse> GetCurrentUser();

    [Post("/users/current")]
    Task<LogOutUserResponse> LogOutUser(LogOutUserRequest request);
}