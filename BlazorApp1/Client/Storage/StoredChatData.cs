using BlazorApp1.Shared.Models;

namespace BlazorApp1.Client.Storage;

public record StoredChatData
{
    public required ChatModel Chat { get; init; }
    public required bool IsPreviewLoaded { get; init; }
}