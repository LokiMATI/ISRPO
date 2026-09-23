using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace UserOptimizationExample
{
    // Модель User
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }

    // Модель Order
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    // Контекст базы данных
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("UserDb");
        }
    }

    // Сервис для работы с пользователями
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public UserService(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // Получение активных пользователей
        public async Task<List<User>> GetActiveUsersAsync()
        {
            // Оптимизация: Использовать AsNoTracking
            var users = await _context.Users
                .Where(u => u.IsActive)
                .ToListAsync();
            return users;
        }

        // Получение пользователей и их заказов
        public async Task<List<User>> GetUsersWithOrdersAsync()
        {
            // Оптимизация: Использовать Select для выборки нужных данных
            var users = await _context.Users
                .Include(u => u.Orders)
                .ToListAsync();
            return users;
        }

        // Массовое добавление пользователей
        public async Task AddUsersAsync(List<User> users)
        {
            foreach (var user in users)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // Оптимизация: использовать AddRange и одну транзакцию
            }
        }
    }

    // Класс Program для запуска приложения
    class Program
    {
        static async Task Main(string[] args)
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
                new User { Name = "Alice", Email = "alice@example.com", IsActive = true },
                new User { Name = "Bob", Email = "bob@example.com", IsActive = false },
                new User { Name = "Charlie", Email = "charlie@example.com", IsActive = true }
            };

            await userService.AddUsersAsync(usersToAdd);

            // Получение активных пользователей
            var activeUsers = await userService.GetActiveUsersAsync();
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
}
