using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.DTO.Yclients;

namespace WellnessMassageCenter.Backend.Services;

public class YclientsService(
    IDbContextFactory<DbWellnessMassageCenterContext> contextFactory,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    IMemoryCache cache,
    IMapper mapper)
{
    private readonly IConfiguration yclientsConfiguration = configuration.GetSection("Yclients");
    private readonly int companyId = configuration.GetSection("Yclients").GetValue<int>("CompanyId");

    public async Task SynchronizeDataAsync()
    {
        var client = httpClientFactory.CreateClient("Yclients");

        if (!cache.TryGetValue("AuthorizationToken", out string? authorizationToken))
        {
            var userToken = await GetUserAuthorizationToken();

            var authorization = client.DefaultRequestHeaders.Authorization
                ?? throw new NullReferenceException("У клиента http 'Yclients' отсутствует заголовок авторизации");

            var bearerToken = client.DefaultRequestHeaders.Authorization.Parameter;
            if (bearerToken is null)
                throw new NullReferenceException("Отсутствует токен авторизации Yclients в заголовке авторизации");

            authorizationToken = $"Bearer {bearerToken}, User {userToken}";
            cache.Set("AuthorizationToken", authorizationToken);
        }

        client.DefaultRequestHeaders.Remove("Authorization");
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationToken);

        using var context = await contextFactory.CreateDbContextAsync();

        await context.Employees.ExecuteDeleteAsync();
        await context.Positions.ExecuteDeleteAsync();
        await context.Services.ExecuteDeleteAsync();
        await context.Categories.ExecuteDeleteAsync();

        await SynchronizePositions(client, context);
        await SynchronizeCategories(client, context);
        await SynchronizeEmployees(client, context);
        await SynchronizeServices(client, context);
    }

    private async Task SynchronizeCategories(HttpClient client, DbWellnessMassageCenterContext context)
    {
        var data = await GetYclientsData<List<YclientsCategoryDto>>(client, $"/api/v1/company/{companyId}/service_categories");
        var categories = mapper.Map<List<Category>>(data);

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }

    private async Task SynchronizePositions(HttpClient client, DbWellnessMassageCenterContext context)
    {
        var data = await GetYclientsData<List<YclientsPositionDto>>(client, $"/api/v1/company/{companyId}/staff/positions");
        var positions = mapper.Map<List<Position>>(data);

        await context.Positions.AddRangeAsync(positions);
        await context.SaveChangesAsync();
    }

    private async Task SynchronizeEmployees(HttpClient client, DbWellnessMassageCenterContext context)
    {
        var data = await GetYclientsData<List<YclientsEmployeeDto>>(client, $"/api/v1/company/{companyId}/staff");
        var employees = mapper.Map<List<Employee>>(data);

        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();
    }

    private async Task SynchronizeServices(HttpClient client, DbWellnessMassageCenterContext sharedContext)
    {
        var data = await GetYclientsData<List<YclientsServiceDto>>(client, $"/api/v1/company/{companyId}/services");

        var uniqueServiceDtos = data
            .GroupBy(dto => dto.Id)
            .Select(group => group.First())
            .ToList();

        var existingEmployees = await sharedContext.Employees.AsNoTracking().ToDictionaryAsync(e => e.Id);

        var servicesToInsert = new List<Service>();

        foreach (var dto in uniqueServiceDtos)
        {
            var service = mapper.Map<Service>(dto);

            var trackedEmployees = dto.Staff
                .Select(s => s.Id)
                .Where(existingEmployees.ContainsKey)
                .Select(id => existingEmployees[id])
                .ToList();

            service.Employees = trackedEmployees;

            servicesToInsert.Add(service);
        }

        using var insertContext = await contextFactory.CreateDbContextAsync();
        foreach (var employee in existingEmployees.Values)
            insertContext.Employees.Attach(employee);

        await insertContext.Services.AddRangeAsync(servicesToInsert);
        await insertContext.SaveChangesAsync();
    }

    private async Task<string> GetUserAuthorizationToken()
    {
        var authorization = yclientsConfiguration.GetSection("Authorization").Get<YclientsAuthorizationDto>();

        if (authorization is null || string.IsNullOrWhiteSpace(authorization.Login) || string.IsNullOrWhiteSpace(authorization.Password))
            throw new NullReferenceException("Не были найдены данные логина и пароля для авторизации на Yclients");

        var content = JsonContent.Create(authorization);

        var client = httpClientFactory.CreateClient("Yclients");
        using var response = await client.PostAsync("/api/v1/auth", content);
        var jsonString = await response.Content.ReadAsStringAsync() ?? throw new NullReferenceException("Отсутствует тело ответа авторизации Yclients");
        var body = JsonNode.Parse(jsonString);

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException(body!.ToJsonString());

        var token = (string?)body!["data"]!["user_token"];
        if (token is null)
            throw new NullReferenceException("Отсутствует токен авторизации пользователя Yclients в теле ответа");

        return token;
    }

    private async Task<T> GetYclientsData<T>(HttpClient client, string requestUri)
    {
        using var response = await client.GetAsync(requestUri);
        var jsonString = await response.Content.ReadAsStringAsync() ?? throw new NullReferenceException("Отсутствует тело ответа");
        var body = JsonNode.Parse(jsonString);

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException(body!["meta"]!.ToJsonString());

        var data = body!["data"]!.Deserialize<T>() ?? throw new NullReferenceException("Отсутствует массив данных");
        return data;
    }
}
