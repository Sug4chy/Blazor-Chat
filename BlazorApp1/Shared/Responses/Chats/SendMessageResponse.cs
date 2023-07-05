using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses;

public record SendMessageResponse
{
    public required MessageModel Message { get; init; }
}