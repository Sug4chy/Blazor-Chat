using System.Security.Claims;
using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Shared.Requests.Chats;

public record CreateChatRequest : IRequest<CreateChatResponse>
{
    public ClaimsPrincipal? User { get; init; }
    public required string Name { get; init; }
}