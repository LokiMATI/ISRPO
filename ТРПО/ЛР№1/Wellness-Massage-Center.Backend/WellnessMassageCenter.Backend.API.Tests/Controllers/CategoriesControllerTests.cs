using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure.Fixtures;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Categories;

namespace WellnessMassageCenter.Backend.API.Tests.Controllers;

public class CategoriesControllerTests(WebApiTestFixture factory) : BaseWebApiTest(factory, "api/categories")
{
    [Fact]
    public async Task GetList_IncludeAllNestedObjectsIsTrue_Code200AndDtoListOfCategoriesWithAllNestedObjects()
    {
        // Arrange
        Uri uri = new(QueryHelpers.AddQueryString(ControllerUrl, "isIncludeSubobjects", "true"), UriKind.Relative);

        var factory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await factory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .ThenInclude(s => s.Employees)
            .ThenInclude(e => e.Position)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<CategoryDto>>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();

        result.Should().NotBeNullOrEmpty()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Fact]
    public async Task GetList_IncludeAllNestedObjectsIsFalse_Code200AndDtoListOfCategoriesWithoutAllNestedObjects()
    {
        // Arrange
        Uri uri = new(ControllerUrl, UriKind.Relative);

        var factory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await factory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .ToListAsync();

        var expectedDtos = mapper.Map<List<CategoryDto>>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();

        result.Should().NotBeNullOrEmpty()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    public async Task Get_WithValidDataAndIncludeAllNestedObjectsIsTrue_Code200AndDtoOfCategoryWithAllNestedObjects(int id)
    {
        // Arrange
        Uri uri = new(QueryHelpers.AddQueryString($"{ControllerUrl}/{id}", "isIncludeSubobjects", "true"), UriKind.Relative);

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
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CategoryDto>();

        result.Should().NotBeNull()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(1)]
    public async Task Get_WithValidDataAndIncludeAllNestedObjectsIsFalse_Code200AndDtoOfCategoryWithoutAllNestedObjects(int id)
    {
        // Arrange
        Uri uri = new($"{ControllerUrl}/{id}", UriKind.Relative);

        var dbFactory = GetService<IDbContextFactory<DbWellnessMassageCenterContext>>();
        var mapper = GetService<IMapper>();
        using var context = await dbFactory.CreateDbContextAsync();
        var entities = await context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .FirstAsync(c => c.Id == id);

        var expectedDtos = mapper.Map<CategoryDto>(entities);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<CategoryDto>();

        result.Should().NotBeNull()
           .And.BeEquivalentTo(expectedDtos, options => options.IgnoringCyclicReferences());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    public async Task Get_WithInvalidDataAndIncludeAllNestedObjectsIsTrue_Code200AndDtoOfCategoryWithAllNestedObjects(int id)
    {
        // Arrange
        Uri uri = new(QueryHelpers.AddQueryString($"{ControllerUrl}/{id}", "isIncludeSubobjects", "true"), UriKind.Relative);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    public async Task Get_WithInvalidDataAndIncludeAllNestedObjectsIsFalse_Code200AndDtoOfCategoryWithoutAllNestedObjects(int id)
    {
        // Arrange
        Uri uri = new($"{ControllerUrl}/{id}", UriKind.Relative);

        // Act
        var response = await Client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
