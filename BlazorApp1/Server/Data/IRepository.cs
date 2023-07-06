using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Data;

public interface IRepository<T> where T : class
{
    public Task AddItemAsync(T newItem);
    public Task DeleteItemAsync(T item);
    public Task UpdateItemAsync(T item);
    public Task<DbSet<T>> GetTableAsync();
}