using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UserOptimizationExample.DbContexts;
using UserOptimizationExample.Models;

namespace UserOptimizationExample.Services;

public class UserService(AppDbContext context, IMemoryCache cache)
{
    private readonly AppDbContext _context = context;
    private readonly IMemoryCache _cache = cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(10);

    // Получение активных пользователей
    public async Task<List<User>> GetActiveUsersAsync()
    {
        var users = await _context.Users
            .Where(u => u.IsActive)
            .AsNoTracking()
            .ToListAsync();
        return users;
    }

    public async Task<List<User>> GetCachedActiveUsersAsync()
    {
        if (!_cache.TryGetValue("ActiveUsers", out List<User>? users) && users is null)
        {
            users = await GetActiveUsersAsync();

            _cache.Set("ActiveUsers", users, _cacheDuration);
        }

        return users;
    }

    // Получение пользователей и их заказов
    public async Task<List<User>> GetUsersWithOrdersAsync()
    {
        // Оптимизация: Использовать Select для выборки нужных данных
        var users = await _context.Users
            //.Include(u => u.Orders)
            .Select(u => new User
            {
                Name = u.Name,
                Orders = u.Orders
            })
            .ToListAsync();
        return users;
    }

    // Массовое добавление пользователей
    public async Task AddUsersAsync(List<User> users)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
        }
        await transaction.CommitAsync();
    }
}
