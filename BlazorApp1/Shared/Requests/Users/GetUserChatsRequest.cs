﻿using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record GetUserChatsRequest : IRequest<GetUserChatsResponse>
{
    public required int UserId { get; init; }
}