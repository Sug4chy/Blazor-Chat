﻿using BlazorApp1.Shared.Responses.Users;
using MediatR;

namespace BlazorApp1.Shared.Requests.Users;

public record CreateUserRequest : IRequest<CreateUserResponse>
{
    public required string Name { get; init; }
    public required string Password { get; init; }
}