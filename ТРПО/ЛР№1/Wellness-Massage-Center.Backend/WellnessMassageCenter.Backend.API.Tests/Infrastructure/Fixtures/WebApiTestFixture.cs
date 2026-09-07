using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.Services.Mappers;

[assembly: WebApplicationFactoryContentRoot(
    "WellnessMassageCenter.Backend.API",
    "../../../../WellnessMassageCenter.Backend.API",
    "WellnessMassageCenter.Backend.API.csproj",
    "0")]

namespace WellnessMassageCenter.Backend.API.Tests.Infrastructure.Fixtures;

public class WebApiTestFixture : WebApplicationFactory<Program>, IDisposable
{
    private readonly SqliteConnection _keepAliveConnection;

    public WebApiTestFixture()
    {
        _keepAliveConnection = new("DataSource=:memory:");
        _keepAliveConnection.Open();

        ClientOptions.BaseAddress = new("http://localhost:5000");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DbWellnessMassageCenterContext>));
            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContextFactory<DbWellnessMassageCenterContext>(options =>
                options.UseSqlite(_keepAliveConnection));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbWellnessMassageCenterContext>();

            InitDatabaseSchema(context);
        });
    }

    private void InitDatabaseSchema(DbWellnessMassageCenterContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.ChangeTracker.AutoDetectChangesEnabled = false;

        Position position = new() { Id = 1, Title = "Специалист по массажу" };
        Category category = new() { Id = 1, Title = "Массаж тела" };

        context.Positions.Add(position);
        context.Categories.Add(category);
        context.SaveChanges();

        context.ChangeTracker.AutoDetectChangesEnabled = false;
        List<Employee> employees = [
            new()
            {
                Id = 1,
                Name = "Тестов Тест Тестович",
                Specialization = "Специалист по массажу",
                Information = "Тестовая информация",
                PositionId = 1,
                Rating = 5,
                Avatar = "http://test.images.com/1.jpg",
                AvatarBig = "http://test.images.com/full_1.jpg",
                IsHidden = false
            },
            new()
            {
                Id = 2,
                Name = "Тестова Теста Тестовична",
                Specialization = "Специалист по массажу",
                Information = "Тестовая информация",
                PositionId = 1,
                Rating = 4,
                Avatar = "http://test.images.com/2.jpg",
                AvatarBig = "http://test.images.com/full_2.jpg",
                IsHidden = false
            },
            new()
            {
                Id = 3,
                Name = "Тестков Тестив Тестовичев",
                Specialization = "Специалист по массажу",
                Information = "Тестовая информация",
                PositionId = 1,
                Rating = 5,
                Avatar = "http://test.images.com/3.jpg",
                AvatarBig = "http://test.images.com/full_3.jpg",
                IsHidden = false
            }
        ];

        employees.ForEach(e =>
        {
            context.Employees.Attach(e);
            context.Entry(e).State = EntityState.Added;
        });

        context.SaveChanges();

        List<Service> services = [
            new() { Id = 1, Title = "Массаж спины", Comment = "Массажируют и ломают спину", Duration = 3600, IsCanRegisteredOnline = true, CategoryId = 1 },
            new() { Id = 2, Title = "Массаж ног", Comment = "Массажируют и ломают ноги", Duration = 1800, IsCanRegisteredOnline = true, CategoryId = 1 },
            new() { Id = 3, Title = "Массаж лица", Comment = "Массажируют и ломают лицо", Duration = 4000, IsCanRegisteredOnline = true, CategoryId = 1 }
        ];

        services.ForEach(s =>
        {
            context.Services.Attach(s);
            context.Entry(s).State = EntityState.Added;
        });

        context.SaveChanges();

        context.ChangeTracker.AutoDetectChangesEnabled = true;

        var dbEmployees = context.Employees.ToList();
        var dbServices = context.Services.ToList();

        dbServices.ForEach(s => s.Employees = [.. dbEmployees]);

        context.SaveChanges();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _keepAliveConnection?.Close();
            _keepAliveConnection?.Dispose();
        }
    }
}
