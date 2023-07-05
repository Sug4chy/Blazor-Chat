namespace BlazorApp1.Shared.Requests;

public record SendMessageRequest
{
    public required int UserId { get; init; }
    public required int ChatId { get; init; }
    public required string Text { get; init; }
}