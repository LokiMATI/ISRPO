using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.Services;
using WellnessMassageCenter.Backend.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

var originsString = builder.Configuration["AllowedOrigins"]
        ?? throw new ArgumentException("Информация о допустимых сайтах в конфигурации не найдена.");
var allowedOrigins = originsString
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(url => url.Trim())
    .ToArray() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddCollectionMappers();
    cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
});
builder.Services.AddHttpClient("Yclients", client =>
{
    var baseAddress = builder.Configuration.GetSection("Yclients")["BaseAddress"];
    if (string.IsNullOrWhiteSpace(baseAddress))
        throw new ArgumentException("Базовый адрес Yclient не должен быть пуст");
    client.BaseAddress = new(baseAddress);

    var headers = builder.Configuration.GetSection("Yclients").GetSection("Headers");

    var accept = headers["Accept"];
    if (string.IsNullOrWhiteSpace(accept))
        throw new ArgumentException("Информация о заголовке 'Accept' не найдена в конфигурации.");
    client.DefaultRequestHeaders.Add("Accept", accept);

    var bearerToken = headers["BearerToken"];
    if (string.IsNullOrWhiteSpace(bearerToken))
        throw new ArgumentException("Информация о заголовке 'BearerToken' не найдена в конфигурации.");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
});
builder.Services.AddDbContextFactory<DbWellnessMassageCenterContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("Default"), ServerVersion.Parse("8.0.46-mysql")));

builder.Services.AddScoped<YclientsService>();
builder.Services.AddScoped<EmployeesService>();
builder.Services.AddScoped<PositionsService>();
builder.Services.AddScoped<MassageServicesService>();
builder.Services.AddScoped<CategoriesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontendApp");
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
