using System.Security.Claims;
using BlazorApp1.Server.Services.Interfaces;
using BlazorApp1.Shared.Requests.Users;
using BlazorApp1.Shared.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Server.Handlers.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUserService _userService;

    public DeleteUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(request.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUser(userId);
        if (user is null)
        {
            throw new ArgumentException($"Пользователя с id {userId} не существует");
        }

        await _userService.DeleteUser(user);
        return new DeleteUserResponse();
    }
}