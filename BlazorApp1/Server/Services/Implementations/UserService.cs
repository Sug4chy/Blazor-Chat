using System.Security.Cryptography;
using System.Text;
using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services.Implementations;

public class UserService : IUserService
{
	private const string Salt = "BLAZORAPP1";
    private readonly IRepository<User> _userDb;

    public UserService(IRepository<User> userDb)
    {
        _userDb = userDb;
    }

    public async Task<User> CreateUser(string name, string password)
    {
	    var user = new User
        {
            Name = name,
            PasswordHash = HashPassword(password)
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

    public async Task DeleteUser(User user)
    {
        await _userDb.DeleteItemAsync(user);
    }

    public string HashPassword(string password)
    { 
	    var passwordBytes = Encoding.UTF8.GetBytes(password + Salt);
	    var passwordHash = SHA256.HashData(passwordBytes);
        return Encoding.UTF8.GetString(passwordHash);
    }
}