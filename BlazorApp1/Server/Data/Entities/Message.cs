namespace BlazorApp1.Server.Data.Entities;

public record Message
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required int SenderId { get; set; }
    public User? Sender { get; set; }
    public required DateTimeOffset SendTime { get; set; }
    public required int ChatId { get; set; }
    public Chat? Chat { get; set; }
}