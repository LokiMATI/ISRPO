using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Categories;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.DTO.Services;

namespace WellnessMassageCenter.Backend.Services;

/// <summary>
/// Сервис категорий услуг
/// </summary>
/// <param name="context">Контекст EFCore</param>
/// <param name="mapper">Маппер объектов</param>
public class CategoriesService(
    DbWellnessMassageCenterContext context,
    IMapper mapper)
{
    /// <summary>
    /// Получить список категорий
    /// </summary>
    /// <param name="isIncludeAllNestedObjects">Подгрузить ли все рекурсивно вложенные объекты</param>
    /// <returns>Список категорий</returns>
    public async Task<IEnumerable<CategoryDto>> GetListAsync(bool isIncludeAllNestedObjects = false)
    {
        var queryable = context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .AsQueryable();

        if (isIncludeAllNestedObjects)
            queryable = queryable
                .Include(c => c.Services)
                .ThenInclude(s => s.Employees)
                .ThenInclude(e => e.Position);


        return mapper.Map<List<CategoryDto>>(await queryable.ToListAsync());
    }

    /// <summary>
    /// Получить информацию об категории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="isIncludeAllNestedObjects">Подгрузить ли все рекурсивно вложенные объекты</param>
    /// <returns>В случае нахождения возвращается объект категории, в противном случае возвращается null</returns>
    public async Task<CategoryDto?> GetByIdAsync(int id, bool isIncludeAllNestedObjects = false)
    {
        var queryable = context.Categories
            .AsNoTracking()
            .Include(c => c.Services)
            .AsQueryable();

        if (isIncludeAllNestedObjects)
            queryable = queryable
                .Include(c => c.Services)
                .ThenInclude(s => s.Employees)
                .ThenInclude(e => e.Position);

        var category = await queryable.FirstOrDefaultAsync(c => c.Id == id);
        if (category is null)
            return null;

        return mapper.Map<CategoryDto>(category);
    }
}
