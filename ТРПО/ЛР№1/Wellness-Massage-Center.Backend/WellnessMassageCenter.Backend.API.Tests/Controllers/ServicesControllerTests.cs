using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure.Fixtures;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Services;

namespace WellnessMassageCenter.Backend.API.Tests.Controllers;

public class ServicesControllerTests(WebApiTestFixture factory) : BaseWebApiTest(factory, "/api/services")
{
    [Fact]
    public async Task GetList_WhenDataExists_Code200AndDtoListOfServices()
    {
        // Arrange
        Uri uri = new(ControllerUrl, UriKind.Relative);

        var factory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await factory.CreateDbContextAsync();
        var entities = await context.Services
            .AsNoTracking()
            .Include(c => c.Employees)
            .ThenInclude(s => s.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<ServiceDto>>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<ServiceDto>>();

        result.Should().NotBeNullOrEmpty()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Get_WithValidData_Code200AndDtoOfService(int id)
    {
        // Arrange
        Uri uri = new($"{ControllerUrl}/{id}", UriKind.Relative);

        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Services
            .AsNoTracking()
            .Include(c => c.Employees)
            .ThenInclude(s => s.Position)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<ServiceDto>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ServiceDto>();

        result.Should().NotBeNull()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(4)]
    public async Task Get_WithInvalidData_Code404AndResponseMessage(int id)
    {
        // Arrange
        Uri uri = new($"{ControllerUrl}/{id}", UriKind.Relative);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var result = await response.Content.ReadAsStringAsync();

        result.Should().NotBeNull().And.BeEquivalentTo("Услуги с таким идентификатором не существует");
    }
}
