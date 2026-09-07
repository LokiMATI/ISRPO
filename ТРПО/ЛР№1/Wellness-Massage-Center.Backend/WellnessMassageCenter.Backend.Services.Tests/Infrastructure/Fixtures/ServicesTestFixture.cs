using AutoMapper.EquivalencyExpression;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.Services.Mappers;

namespace WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

public class ServicesTestFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; private set; }

    public IConfiguration Configuration { get; private set; }

    private readonly SqliteConnection _keepAliveConnection;

    public ServicesTestFixture()
    {
        ServiceCollection services = new();

        _keepAliveConnection = new("DataSource=:memory:");
        _keepAliveConnection.Open();
        services.AddDbContextFactory<DbWellnessMassageCenterContext>(options =>
            options.UseSqlite(_keepAliveConnection));

        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddCollectionMappers();
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        });
        services.AddMemoryCache();
        services.AddScoped<YclientsService>();
        services.AddScoped<EmployeesService>();
        services.AddScoped<PositionsService>();
        services.AddScoped<MassageServicesService>();
        services.AddScoped<CategoriesService>();

        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .Build();
        services.AddSingleton(Configuration);

        ServiceProvider = services.BuildServiceProvider();

        InitDatabaseSchema();
    }

    private void InitDatabaseSchema()
    {
        var factory = ServiceProvider.GetRequiredService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        using var context = factory.CreateDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Position position = new()  { Id = 1, Title = "Специалист по массажу" };
        Category category = new()  { Id = 1, Title = "Массаж тела" };

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

        dbServices.ForEach(s => s.Employees =  dbEmployees);

        context.SaveChanges();
    }

    public void Dispose()
    {
        _keepAliveConnection?.Close();
        _keepAliveConnection?.Dispose();

        if (ServiceProvider is IDisposable disposable)
            disposable.Dispose();
    }
}
