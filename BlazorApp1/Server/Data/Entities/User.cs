namespace BlazorApp1.Server.Data.Entities;

public record User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string HashPassword { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<Chat> Chatrooms { get; set; } = new List<Chat>();
}