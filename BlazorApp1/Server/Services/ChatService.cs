using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services;

public class ChatService
{
    private readonly ApplicationContext _db;

    public ChatService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Chat> CreateChat(string name)
    {
        var chat = new Chat
        {
            Name = name
        };
        await _db.Chatrooms.AddAsync(chat);
        await _db.SaveChangesAsync();
        return chat;
    }

    public async Task<Chat?> GetChat(int chatId)
    {
        var chat = await _db.Chatrooms
            .Include(chat => chat.Messages)
            .Include(chat => chat.Users)
            .FirstAsync(chat => chat.Id == chatId);
        return chat;
    }

    public async Task AddUserInChat(User user, Chat chat)
    {
        chat.Users.Add(user);
        _db.Update(user);
        user.Chatrooms.Add(chat);
        _db.Update(chat);
        await _db.SaveChangesAsync();
    }
}