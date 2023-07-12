using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Shared.Requests.Chats;

public record SendMessageRequest : IRequest<SendMessageResponse>
{
    public required int UserId { get; init; }
    public required int ChatId { get; init; }
    public required string Text { get; init; }
}