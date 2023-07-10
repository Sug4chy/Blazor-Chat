using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services.Interfaces;

public interface IUserService
{
    public Task<User> CreateUser(string name, string password);
    public Task<IReadOnlyCollection<User>> GetAllUsers();
    public Task<User?> GetUser(int id);
    public Task DeleteUser(User user);
}