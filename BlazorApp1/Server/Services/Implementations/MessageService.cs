using BlazorApp1.Server.Data;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;

namespace BlazorApp1.Server.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IRepository<Message> _messageDb;

    public MessageService(IRepository<Message> messageDb)
    {
        _messageDb = messageDb;
    }

    public async Task<Message> CreateMessage(string text, User sender, Chat chat)
    {
        var message = new Message
        {
            Text = text,
            ChatId = chat.Id,
            SenderId = sender.Id,
            SendTime = DateTimeOffset.Now
        };
        
        chat.Messages.Add(message);
        sender.Messages.Add(message);

        await _messageDb.AddItemAsync(message);
        return message;
    }
}