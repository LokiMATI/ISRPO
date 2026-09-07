using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Categories;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.Services.Tests.Services;

public class CategoriesServiceTests(ServicesTestFixture fixture) : BaseServiceTest(fixture)
{
    [Fact]
    public async Task GetListAsync_IncludeAllNestedObjectsIsTrue_DtoListOfCategoriesWithAllNestedObjects()
    {
        // Arrange
        var service = GetService<CategoriesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .ThenInclude(s => s.Employees)
            .ThenInclude(e => e.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<CategoryDto>>(entities);

        // Act
        var result = await service.GetListAsync(true);

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Fact]
    public async Task GetListAsync_IncludeAllNestedObjectsIsFalse_DtoListOfCategoriesWithoutAllNestedObjects()
    {
        // Arrange
        var service = GetService<CategoriesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<CategoryDto>>(entities);

        // Act
        var result = await service.GetListAsync();

        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    public async Task GetByIdAsync_WithValidDataAndIncludeAllNestedObjectsIsTrue_DtoOfCategoryWithAllNestedObjects(int id)
    {
        // Arrange
        var service = GetService<CategoriesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .ThenInclude(s => s.Employees)
            .ThenInclude(e => e.Position)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<CategoryDto>(entities);

        // Act
        var result = await service.GetByIdAsync(id, true);

        // Assert
        result.Should().NotBeNull()
            .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    public async Task GetByIdAsync_WithInvalidDataAndIncludeAllNestedObjectsIsTrue_Null(int id)
    {
        // Arrange
        var service = GetService<CategoriesService>();

        // Act
        var result = await service.GetByIdAsync(id, true);

        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(1)]
    public async Task GetByIdAsync_WithValidDataAndIncludeAllNestedObjectsIsFalse_DtoOfCategoryWithoutAllNestedObjects(int id)
    {
        // Arrange
        var service = GetService<CategoriesService>();
        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<CategoryDto>(entities);

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
    public async Task GetByIdAsync_WithInvalidDataAndIncludeAllNestedObjectsIsFalse_Null(int id)
    {
        // Arrange
        var service = GetService<CategoriesService>();

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}
