using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp1.Client;

public abstract class HubClientBase
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    protected abstract string HubRelativeUrl { get; }
    public HubConnection Connection { get; }

    protected HubClientBase(NavigationManager navigationManager, IAccessTokenProvider accessTokenProvider)
    {
        _accessTokenProvider = accessTokenProvider;
        var hubUri = navigationManager.ToAbsoluteUri(HubRelativeUrl);
        Connection = new HubConnectionBuilder().WithUrl(hubUri, options =>
        {
            options.AccessTokenProvider = RetrieveToken;
        }).Build();
    }

    public Task StartAsync() => Connection.State is HubConnectionState.Disconnected
        ? Connection.StartAsync()
        : Task.CompletedTask;

    private async Task<string?> RetrieveToken()
    {
        var tokenResult = await _accessTokenProvider.RequestAccessToken();
        tokenResult.TryGetToken(out var token);
        return token?.Value;
    }
}