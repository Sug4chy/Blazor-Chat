using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services;

public class ChatService
{
    private readonly ApplicationContext _db;

    public ChatService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Chatroom> CreateChat(string name)
    {
        var chat = new Chatroom
        {
            Name = name
        };
        await _db.Chatrooms.AddAsync(chat);
        await _db.SaveChangesAsync();
        return chat;
    }

    public async Task<Chatroom?> GetChat(int chatId)
    {
        var chat = await  _db.Chatrooms.FindAsync(chatId);
        return chat;
    }
}