using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services.Interfaces;

public interface IChatService
{
    public Task<Chat> CreateChat(string name);
    public Task<Chat?> GetChat(int chatId);
    public Task AddUserInChat(User user, Chat chat);
    public Task<IReadOnlyCollection<Chat>> GetAllChats();
    public Task DeleteChat(Chat chat);
}