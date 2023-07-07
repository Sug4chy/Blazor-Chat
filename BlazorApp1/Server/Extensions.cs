using BlazorApp1.Server.Data;
using BlazorApp1.Server.Services.Implementations;
using BlazorApp1.Server.Services.Interfaces;

namespace BlazorApp1.Server;

public static class Extensions
{
    public static void AddTransientServices(this IServiceCollection collection)
    {
        collection.Scan(scan => scan
            .FromAssemblyOf<IChatService>()
            .AddClasses(classes => classes.AssignableTo<IChatService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<IUserService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<IMessageService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces());
    }
}