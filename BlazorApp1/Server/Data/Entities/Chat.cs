namespace BlazorApp1.Server.Data.Entities;

public record Chat
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}