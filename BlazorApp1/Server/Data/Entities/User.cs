namespace BlazorApp1.Server.Data.Entities;

public record User
{
    public int Id { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<Chat> Chatrooms { get; set; } = new List<Chat>();
    public required string Name { get; set; }
}