using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses.Users;

public record GetAllUsersResponse
{
    public required UserModel[] AllUsers { get; init; }
}