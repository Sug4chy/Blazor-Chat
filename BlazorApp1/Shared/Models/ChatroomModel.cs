namespace BlazorApp1.Shared.Models;

public record ChatroomModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public MessageModel[] Messages { get; set; } = Array.Empty<MessageModel>();
    public UserModel[] Users { get; set; } = Array.Empty<UserModel>();
}