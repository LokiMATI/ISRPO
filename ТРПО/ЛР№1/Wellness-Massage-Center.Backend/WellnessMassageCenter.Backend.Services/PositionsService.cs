using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.DTO.Positions;

namespace WellnessMassageCenter.Backend.Services;

/// <summary>
/// Сервис должностей
/// </summary>
/// <param name="context">Контекст EFCore</param>
/// <param name="mapper">Маппер объектов</param>
public class PositionsService(
    DbWellnessMassageCenterContext context,
    IMapper mapper)
{
    /// <summary>
    /// Получить список должностей
    /// </summary>
    /// <returns>Список должностей</returns>
    public async Task<IEnumerable<PositionDto>> GetListAsync()
    {
        var positions = await context.Positions.AsNoTracking().ToListAsync();
        return mapper.Map<List<PositionDto>>(positions);
    }

    /// <summary>
    /// Получить должность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>В случае нахождения возвращается объект должности, в противном случае возвращается null</returns>
    public async Task<PositionDto?> GetByIdAsync(int id)
    {
        var position = await context.Positions.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (position is null)
            return null;

        return mapper.Map<PositionDto>(position);
    }
}
