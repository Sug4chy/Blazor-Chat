using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses;

public record GetChatResponse
{
    public required ChatroomModel Chat { get; init; }
}