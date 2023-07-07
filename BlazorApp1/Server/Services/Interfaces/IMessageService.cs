using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services.Implementations;

public interface IMessageService
{
    public Task<Message> CreateMessage(string text, User sender, Chat chat);
}