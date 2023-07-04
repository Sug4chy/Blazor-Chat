namespace BlazorApp1.Server.Data.Entities;

public record User
{
    public int Id { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<Chatroom> Chatrooms { get; set; } = new List<Chatroom>();
    public required string Name { get; set; }
}