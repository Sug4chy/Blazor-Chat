using BlazorApp1.Server.Data;
using BlazorApp1.Server.Extensions;
using BlazorApp1.Server.Hubs;
using BlazorApp1.Server.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlite("Data source=app.db");
        });

        serviceCollection.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
        });

        serviceCollection.AddSignalR();

        serviceCollection.AddControllersWithViews();
        serviceCollection.AddRazorPages();
        serviceCollection.AddSwaggerGen();

        serviceCollection.AddDefaultServices();
        serviceCollection.AddAutoMapper(typeof(AppMappingProfile));
        serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
            });
        serviceCollection.AddMediatR(mediatr =>
        {
            mediatr.RegisterServicesFromAssemblyContaining<Program>();
        });
        serviceCollection.AddErrorHandling();
    }

    public static void Configure(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.MigrateAsync();

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
        app.UseErrorHandling();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.MapHub<ChatHub>("api/hubs/chat");

        app.Run();
    }
}