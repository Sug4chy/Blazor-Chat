using System.Security.Claims;
using AutoMapper;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Models;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(
        IUserService userService, 
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<CreateUserResponse> CreateUser([FromForm] CreateUserRequest request)
	{
        var entity = await _userService.CreateUser(request.Name, request.Password);

        // AUTHENTICATION-->
		var claim = new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString());
		var identity = new ClaimsIdentity(new[] { claim }, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		// <--AUTHENTICATION

		var model = _mapper.Map<UserModel>(entity);
        return new CreateUserResponse { User = model };
    }

    [HttpDelete]
    [Authorize]
    public async Task<DeleteUserResponse> DeleteUser([FromQuery] DeleteUserRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); //Можно вынести в метод расширения ClaimsPrincipal


        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        await _userService.DeleteUser(user);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return new DeleteUserResponse();
    }
    
    [HttpGet]
    public async Task<GetAllUsersResponse> GetAllUsers([FromQuery] GetAllUsersRequest request)
    {
        var entities = await _userService.GetAllUsers();
        var models = entities.Select(_mapper.Map<UserModel>)
            .ToArray();
        return new GetAllUsersResponse { AllUsers = models };
    }

    [HttpGet("{userId:int}")]
    public async Task<GetUserChatsResponse> GetUserChats([FromRoute, FromBody] GetUserChatsRequest request)
    {
        var user = await _userService.GetUser(request.UserId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {request.UserId} не существует");
        }

        var chats = user.Chatrooms
            .Select(_mapper.Map<ChatModel>)
            .ToArray();
        return new GetUserChatsResponse { UserName = user.Name, Chats = chats };
    }
}