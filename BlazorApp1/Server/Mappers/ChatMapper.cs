using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Server.Mappers;

public class ChatMapper
{
    private readonly MessageMapper _messageMapper;
    private readonly UserMapper _userMapper;

    public ChatMapper(MessageMapper messageMapper, UserMapper userMapper)
    {
        _messageMapper = messageMapper;
        _userMapper = userMapper;
    }

    public ChatModel Map(Chat chat) => new()
    {
        Id = chat.Id,
        Messages = chat.Messages.Select(_messageMapper.Map).ToArray(),
        Name = chat.Name,
        Users = chat.Users.Select(_userMapper.Map).ToArray()
    };
}