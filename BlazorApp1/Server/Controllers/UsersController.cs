﻿using System.Security.Claims;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserRequest request)
    {
        var response = await _mediator.Send(request);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.User!);
        return response with { User = null };
    }

    [HttpPost("auth")]
    public async Task<AuthorizeUserResponse> AuthorizeUser([FromBody] AuthorizeUserRequest request)
    {
        var response = await _mediator.Send(request);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.User!);
        return response with { User = null };
    }
    
    [HttpGet("{userId:int}")]
    [Authorize]
    public Task<GetUserChatsResponse> GetUserChats([FromRoute, FromBody] GetUserChatsRequest request)
        => _mediator.Send(request);

    [HttpGet("current")]
    public Task<GetCurrentUserResponse> GetCurrentUser()
    {
        var a = User.FindFirst(ClaimTypes.NameIdentifier);
        return _mediator.Send(a is null
            ? new GetCurrentUserRequest { User = new NullUser() }
            : new GetCurrentUserRequest { User = User });
    }

    [HttpPost("current")]
    [Authorize]
    public async Task<LogOutUserResponse> LogOutUser(LogOutUserRequest request)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return await _mediator.Send(new LogOutUserRequest { User = User });
    }
}