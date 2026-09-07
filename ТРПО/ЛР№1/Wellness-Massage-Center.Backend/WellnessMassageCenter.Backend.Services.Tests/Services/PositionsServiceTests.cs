using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.Services.Tests.Services;

public class PositionsServiceTests(ServicesTestFixture fixture) : BaseServiceTest(fixture)
{
    [Fact]
    public async Task GetListAsync_WhenDataExists_DtoListOfPositions()
    {
        // Arrange
        var service = GetService<PositionsService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Positions
            .AsNoTracking()
            .ToListAsync();

        var expectedDtos = mapper.Map<List<PositionDto>>(entities);

        // Act
        var result = await service.GetListAsync();

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedDtos);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetByIdAsync_WithValidData_DtoOfPosition(int id)
    {
        // Arrange
        var service = GetService<PositionsService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Positions
            .AsNoTracking()
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<PositionDto>(entities);

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    public async Task GetByIdAsync_WithInvalidData_Null(int id)
    {
        // Arrange
        var service = GetService<PositionsService>();

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}
