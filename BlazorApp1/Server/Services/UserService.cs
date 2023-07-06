using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services;

public class UserService
{
    private readonly IRepository<User> _userDb;

    public UserService(IRepository<User> userDb)
    {
        _userDb = userDb;
    }

    public async Task<User> CreateUser(string name)
    {
        var user = new User
        {
            Name = name
        };
        await _userDb.AddItemAsync(user);
        return user;
    }

    public async Task<IReadOnlyCollection<User>> GetAllUsers()
    {
        var users = await _userDb.GetTableAsync();
        return await users.ToArrayAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        var users = await _userDb.GetTableAsync();
            var user = await users
            .Include(u => u.Chatrooms)
            .FirstAsync(u => u.Id == id);
        return user;
    }
}