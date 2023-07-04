namespace BlazorApp1.Shared.Models;

public record UserModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}