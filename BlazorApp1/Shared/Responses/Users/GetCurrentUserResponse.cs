using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record GetCurrentUserResponse
{
    public UserModel? CurrentUser { get; init; }
}