using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services;

public class UserService
{
    private readonly ApplicationContext _db;

    public UserService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<User> CreateUser(string name)
    {
        var user = new User
        {
            Name = name
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        var user = await _db.Users.FindAsync(id);
        return user;
    }
}