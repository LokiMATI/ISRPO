using DTO.Categories;
using Models;

namespace ServerApi.Services;

public class CategoryService
{
    private readonly List<Category> categories = new() 
    {
        new()
        {
            Id = 0,
            Name = "Хозтовары"
        },
        new()
        {
            Id = 1,
            Name = "Продукты питания"
        },
        new()
        {
            Id = 2,
            Name = "Электроника"
        },
    };

    public async Task<Category?> GetCategoryAsync(int id) => categories.FirstOrDefault(c => c.Id == id);

    public async Task<List<Category>> GetCategoriesAsync() => categories;

    public async Task<Category> AddCategoryAsync(CategoryCreateDto dto)
    {
        Category category = new()
        {
            Id = categories.Count + 1,
            Name = dto.Name
        };

        categories.Add(category);

        return category;
    }

    public async Task<Category?> UpdateCategoryAsync(Category dto)
    {
        var category = categories.FirstOrDefault(c => c.Id == dto.Id);
        if (category is null)
            return null;

        category.Name = dto.Name;

        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = categories.FirstOrDefault(c => c.Id == id);
        if (category is null)
            return false;

        categories.Remove(category);
        return true;
    }
}
