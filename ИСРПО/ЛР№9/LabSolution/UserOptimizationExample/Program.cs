using Microsoft.Extensions.DependencyInjection;
using UserOptimizationExample.DbContexts;
using UserOptimizationExample.Models;
using UserOptimizationExample.Services;

class Program
{
    static async Task Main()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>();
        services.AddMemoryCache();
        services.AddScoped<UserService>();

        var serviceProvider = services.BuildServiceProvider();

        // Инициализация данных и сервисов
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();

        // Добавление пользователей
        var usersToAdd = new List<User>
            {
                new() { Name = "Alice", Email = "alice@example.com", IsActive = true },
                new() { Name = "Bob", Email = "bob@example.com", IsActive = false },
                new() { Name = "Charlie", Email = "charlie@example.com", IsActive = true }
            };

        var badUsersToAdd = new List<User>
            {
                new() { Name = "Test", Email = "charlie@example.com", IsActive = true },
                new() { Name = "Aliceamdlpawdmawopdmopawmdopawmdopawmdopadmpoawdmo", Email = "alice@example.com", IsActive = true },
                new() { Name = "Aliceamdlpawdmawopdmopawmdopawmdopawmdopadmpoawdmo", Email = "bob@example.com", IsActive = false },
            };

        await userService.AddUsersAsync(usersToAdd);

        // Получение активных пользователей
        var activeUsers = await userService.GetCachedActiveUsersAsync();
        await Task.Delay(2000);
        activeUsers = await userService.GetCachedActiveUsersAsync();
        Console.WriteLine("Active Users:");
        foreach (var user in activeUsers)
        {
            Console.WriteLine($"{user.Name} - {user.Email}");
        }

        // Получение пользователей и их заказов
        var usersWithOrders = await userService.GetUsersWithOrdersAsync();
        Console.WriteLine("Users with Orders:");
        foreach (var user in usersWithOrders)
        {
            Console.WriteLine($"{user.Name} - Orders: {user.Orders.Count}");
        }
    }
}