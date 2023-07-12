using BlazorApp1.Server.Data;
using BlazorApp1.Server.Extensions;
using BlazorApp1.Server.Hubs;
using BlazorApp1.Server.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite("Data source=app.db");
});

builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultServices();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });
builder.Services.AddMediatR(mediatr =>
{
    mediatr.RegisterServicesFromAssemblyContaining<Program>();
});

var app = builder.Build();
using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHub<ChatHub>($"/chats");

app.Run();