using BlazorApp1.Shared.Models;

namespace BlazorApp1.Shared.Responses;

public record CreateChatResponse
{
    public required ChatroomModel Chat { get; init; }
}