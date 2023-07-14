using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services.Implementations;

public class UserService : IUserService
{
    private readonly IRepository<User> _userDb;
    private readonly IAuthService _authService;

    public UserService(IRepository<User> userDb, IAuthService authService)
    {
        _userDb = userDb;
        _authService = authService;
    }

    public async Task<User> CreateUser(string name, string password)
    {
        var user = new User
        {
            Name = name,
            HashPassword = await _authService.HashPassword(password)
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
            .Include(u => u.Messages)
            .FirstAsync(u => u.Id == id);
        return user;
    }

    public async Task DeleteUser(User user)
    {
        await _userDb.DeleteItemAsync(user);
    }
}