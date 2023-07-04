using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Server.Mappers;

public class MessageMapper
{
    public MessageModel Map(Message message) => new()
    {
        Id = message.Id,
        SenderId = message.SenderId,
        Text = message.Text,
        ChatId = message.ChatId
    };
}