namespace BlazorApp1.Shared.Models;

public record MessageModel
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required int SenderId { get; set; }
}