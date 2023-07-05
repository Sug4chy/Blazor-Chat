using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services;

public class MessageService
{
    private readonly ApplicationContext _db;

    public MessageService(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Message> CreateMessage(string text, User sender, Chat chat)
    {
        var message = new Message
        {
            Text = text,
            Chat = chat,
            ChatId = chat.Id,
            Sender = sender,
            SenderId = sender.Id,
            SendTime = DateTimeOffset.Now
        };
        
        chat.Messages.Add(message);
        sender.Messages.Add(message);
        
        await _db.Messages.AddAsync(message);
        await _db.SaveChangesAsync();
        return message;
    }
}