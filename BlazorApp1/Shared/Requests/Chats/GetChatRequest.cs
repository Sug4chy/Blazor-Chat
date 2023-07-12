using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Shared.Requests.Chats;

public record GetChatRequest : IRequest<GetChatResponse>
{
    public required int ChatId { get; init; }
}