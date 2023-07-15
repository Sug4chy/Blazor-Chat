using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1.Client;
using BlazorApp1.Client.Storage;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<IChatsControllerClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddRefitClient<IUsersControllerClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddApiAuthorization();
builder.Services.AddScoped<SharedChatDataStorage>();
builder.Services.AddScoped<ChatHubClient>();

await builder.Build().RunAsync();