using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.Services.Tests.Services;

public class EmployeesServiceTests(ServicesTestFixture fixture) : BaseServiceTest(fixture)
{
    [Fact]
    public async Task GetListAsync_WhenDataExists_DtoListOfEmployees()
    {
        // Arrange
        var service = GetService<EmployeesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<EmployeeDto>>(entities);

        // Act
        var result = await service.GetListAsync();

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByIdAsync_WithValidData_DtoOfEmployee(int id)
    {
        // Arrange
        var service = GetService<EmployeesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Employees
            .AsNoTracking()
            .Include(c => c.Position)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<EmployeeDto>(entities);

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(4)]
    public async Task GetByIdAsync_WithInvalidData_Null(int id)
    {
        // Arrange
        var service = GetService<EmployeesService>();

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}
