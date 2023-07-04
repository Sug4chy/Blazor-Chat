using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Server.Mappers;

public class UserMapper
{
    public UserModel Map(User user) => new()
    {
        Id = user.Id,
        Name = user.Name
    };
}