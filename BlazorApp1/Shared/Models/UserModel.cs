namespace BlazorApp1.Shared.Models;

public record UserModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public MessageModel[] Messages { get; set; } = Array.Empty<MessageModel>();
    public ChatroomModel[] Chatrooms { get; set; } = Array.Empty<ChatroomModel>();
}