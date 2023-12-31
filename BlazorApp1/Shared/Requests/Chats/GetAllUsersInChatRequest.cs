﻿using BlazorApp1.Shared.Responses.Chats;
using MediatR;

namespace BlazorApp1.Shared.Requests.Chats;

public record GetAllUsersInChatRequest : IRequest<GetAllUsersInChatResponse>
{
    public required int ChatId { get; init; }
}