using BlazorApp1.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Chatroom> Chatrooms => Set<Chatroom>();
    public DbSet<Message> Messages => Set<Message>();

    public ApplicationContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}