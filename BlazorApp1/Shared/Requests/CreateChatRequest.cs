namespace BlazorApp1.Shared.Requests;

public record CreateChatRequest
{
    public required string Name { get; init; }
}