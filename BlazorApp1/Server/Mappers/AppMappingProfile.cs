using AutoMapper;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Shared.Models;

namespace BlazorApp1.Server.Mappers;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<Chat, ChatModel>();
        CreateMap<Message, MessageModel>();
    }
}