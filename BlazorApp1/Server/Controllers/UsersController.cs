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
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.Principal);
        return response;
    }

    [HttpPost("auth")]
    public async Task<AuthorizeUserResponse> AuthorizeUser([FromBody] AuthorizeUserRequest request)
    {
        var response = await _mediator.Send(request);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.User!);
        return new AuthorizeUserResponse { User = null };
    }

    [HttpDelete]
    [Authorize]
    public async Task<DeleteUserResponse> DeleteUser([FromQuery] DeleteUserRequest request)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var response = await _mediator.Send(request with { User = User });
        return response;
    }

    [HttpGet]
    public Task<GetAllUsersResponse> GetAllUsers([FromQuery] GetAllUsersRequest request) => _mediator.Send(request);

    [HttpGet("{userId:int}")]
    [Authorize]
    public Task<GetUserChatsResponse> GetUserChats([FromRoute, FromBody] GetUserChatsRequest request) => 
        _mediator.Send(request);

    [HttpGet("current")]
    [Authorize]
    public Task<GetCurrentUserResponse> GetCurrentUser([FromQuery] GetCurrentUserRequest request) =>
        _mediator.Send(new GetCurrentUserRequest { User = User });
}