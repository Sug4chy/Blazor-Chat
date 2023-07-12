using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Shared.Requests.Chats;

public record AddUserInChatRequest : IRequest<AddUserInChatResponse>
{
    public required int UserId { get; init; }
    public required int ChatId { get; init; }
}