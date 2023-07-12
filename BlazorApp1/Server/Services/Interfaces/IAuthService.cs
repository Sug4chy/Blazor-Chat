using System.Security.Claims;
using BlazorApp1.Server.Data.Entities;

namespace BlazorApp1.Server.Services.Interfaces;

public interface IAuthService
{
    public Task<string> HashPassword(string password);
    public Task<ClaimsPrincipal> AuthUser(User user);
}