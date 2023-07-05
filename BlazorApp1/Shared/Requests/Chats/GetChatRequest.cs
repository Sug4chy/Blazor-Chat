namespace BlazorApp1.Shared.Requests;

public record GetChatRequest
{
    public required int ChatId { get; init; }
}