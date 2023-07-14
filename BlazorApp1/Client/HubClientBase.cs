using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorApp1.Client;

public abstract class HubClientBase
{
    protected abstract string HubRelativeUrl { get; }
    public HubConnection Connection { get; }

    protected HubClientBase(NavigationManager navigationManager)
    {
        var hubUri = navigationManager.ToAbsoluteUri(HubRelativeUrl);
        Connection = new HubConnectionBuilder().WithUrl(hubUri).Build();
    }

    public Task StartAsync() => Connection.State is HubConnectionState.Disconnected
        ? Connection.StartAsync()
        : Task.CompletedTask;
}