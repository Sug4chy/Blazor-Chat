using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorApp1.Server.Data.Entities;
using BlazorApp1.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorApp1.Server.Services.Implementations;

public class AuthService : IAuthService
{
    private const string Salt = "Ulyulyu";
    
    public async Task<string> HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + Salt);
        var passwordHash = SHA256.HashData(passwordBytes);
        return Encoding.UTF8.GetString(passwordHash);
    }

    public async Task<ClaimsPrincipal> AuthUser(User user)
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        var identity = new ClaimsIdentity(new[] { claim }, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        return principal;
    }
}