using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Services;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.Services.Tests.Services;

public class MassageServicesServiceTests(ServicesTestFixture fixture) : BaseServiceTest(fixture)
{
    [Fact]
    public async Task GetListAsync_WhenDataExists_DtoListOfServices()
    {
        // Arrange
        var service = GetService<MassageServicesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Services
            .AsNoTracking()
            .Include(s => s.Employees)
            .ThenInclude(e => e.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<ServiceDto>>(entities);

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
    public async Task GetByIdAsync_WithValidData_DtoOfService(int id)
    {
        // Arrange
        var service = GetService<MassageServicesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Services
            .AsNoTracking()
            .Include(c => c.Employees)
            .ThenInclude(e => e.Position)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<ServiceDto>(entities);

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
        var service = GetService<MassageServicesService>();

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}
