using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.DTO.Services;

namespace WellnessMassageCenter.Backend.Services;

/// <summary>
/// Сервис улуг
/// </summary>
/// <param name="context">Контекст EFCore</param>
/// <param name="mapper">Маппер объектов</param>
public class MassageServicesService(
    DbWellnessMassageCenterContext context,
    IMapper mapper)
{
    /// <summary>
    /// Получить список услуг
    /// </summary>
    /// <returns>Список услуг</returns>
    public async Task<IEnumerable<ServiceDto>> GetListAsync()
    {
        var services = await context.Services
            .AsNoTracking()
            .Include(s => s.Employees)
            .ThenInclude(e => e.Position)
            .ToListAsync();
        return mapper.Map<List<ServiceDto>>(services);
    }

    /// <summary>
    /// Получить информацию об услуге по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>В случае нахождения возвращается объект услуги, в противном случае возвращается null</returns>
    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var service = await context.Services
            .AsNoTracking()
            .Include(s => s.Employees)
            .ThenInclude(e => e.Position)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (service is null)
            return null;

        return mapper.Map<ServiceDto>(service);
    }
}
