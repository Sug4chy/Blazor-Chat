using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Data;

public class ContextRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationContext _db;

    public ContextRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task AddItemAsync(T newItem)
    {
        await _db.Set<T>().AddAsync(newItem);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(T item)
    {
        _db.Set<T>().Remove(item);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(T item)
    {
        _db.Set<T>().Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task<DbSet<T>> GetTableAsync()
    {
        return _db.Set<T>();
    }
}