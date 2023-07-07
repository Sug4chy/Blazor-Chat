using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services.Implementations;

public interface IChatService
{
    public Task<Chat> CreateChat(string name);
    public Task<Chat> GetChat(int chatId);
    public Task AddUserInChat(User user, Chat chat);
}