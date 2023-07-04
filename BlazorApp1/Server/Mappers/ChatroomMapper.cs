using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Server.Mappers;

public class ChatroomMapper
{
    private readonly MessageMapper _messageMapper;
    private readonly UserMapper _userMapper;

    public ChatroomMapper(MessageMapper messageMapper, UserMapper userMapper)
    {
        _messageMapper = messageMapper;
        _userMapper = userMapper;
    }

    public ChatroomModel Map(Chatroom chatroom) => new()
    {
        Id = chatroom.Id,
        Messages = chatroom.Messages.Select(_messageMapper.Map).ToArray(),
        Name = chatroom.Name,
        Users = chatroom.Users.Select(_userMapper.Map).ToArray()
    };
}