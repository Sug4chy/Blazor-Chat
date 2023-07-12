using BlazorApp1.Server.Data;
using BlazorApp1.Server.Services.Interfaces;

namespace BlazorApp1.Server.Extensions;

public static class DependencyInjection
{
    public static void AddDefaultServices(this IServiceCollection collection)
    {
        collection.Scan(scan =>
        {
            scan.FromAssemblyOf<IChatService>()
                .AddClasses(classes => classes.AssignableTo<IChatService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<IUserService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<IMessageService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<IAuthService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }
}