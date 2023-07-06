using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Services;

public class ChatService
{
    private readonly IRepository<Chat> _chatDb;
    private readonly IRepository<User> _userDb;

    public ChatService(IRepository<Chat> chatDb, IRepository<User> userDb)
    {
        _chatDb = chatDb;
        _userDb = userDb;
    }

    public async Task<Chat> CreateChat(string name)
    {
        var chat = new Chat
        {
            Name = name
        };
        await _chatDb.AddItemAsync(chat);
        return chat;
    }

    public async Task<Chat?> GetChat(int chatId)
    {
        var chats = await _chatDb.GetTableAsync();
        var chat = await chats
            .Include(chat => chat.Messages)
            .Include(chat => chat.Users)
            .FirstAsync(chat => chat.Id == chatId);
        return chat;
    }

    public async Task AddUserInChat(User user, Chat chat)
    {
        chat.Users.Add(user);
        await _userDb.UpdateItemAsync(user);
        user.Chatrooms.Add(chat);
        await _chatDb.UpdateItemAsync(chat);
    }
}