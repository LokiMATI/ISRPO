using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure.Fixtures;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Employees;

namespace WellnessMassageCenter.Backend.API.Tests.Controllers;

public class EmployeesControllerTests(WebApiTestFixture factory) : BaseWebApiTest(factory, "api/employees")
{
    [Fact]
    public async Task GetList_WhenDataExists_Code200AndDtoListOfEmployees()
    {
        // Arrange
        Uri uri = new(ControllerUrl, UriKind.Relative);

        var factory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await factory.CreateDbContextAsync();
        var entities = await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<EmployeeDto>>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

        result.Should().NotBeNullOrEmpty()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Get_WithValidData_Code200AndDtoOfEmployee(int id)
    {
        // Arrange
        Uri uri = new($"{ControllerUrl}/{id}", UriKind.Relative);

        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Employees
            .AsNoTracking()
            .Include(c => c.Position)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<EmployeeDto>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<EmployeeDto>();

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

        result.Should().NotBeNull().And.BeEquivalentTo("Сотрудника с таким идентификатором не существует");
    }
}
