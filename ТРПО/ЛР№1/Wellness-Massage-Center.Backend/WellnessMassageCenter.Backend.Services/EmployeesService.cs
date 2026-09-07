using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WellnessMassageCenter.Backend.DAL.Contexts;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.DTO.Services;

namespace WellnessMassageCenter.Backend.Services;

/// <summary>
/// Сервис сотрудников
/// </summary>
/// <param name="context">Контекст EFCore</param>
/// <param name="mapper">Маппер объектов</param>
public class EmployeesService(
    DbWellnessMassageCenterContext context,
    IMapper mapper)
{
    /// <summary>
    /// Получить список сотрудников
    /// </summary>
    /// <returns>Список сотрудников</returns>
    public async Task<IEnumerable<EmployeeDto>> GetListAsync()
    {
        var employees = await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .ToListAsync();
        return mapper.Map<List<EmployeeDto>>(employees);
    }

    /// <summary>
    /// Получить информацию об сотруднике по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>В случае нахождения возвращается объект сотрудника, в противном случае возвращается null</returns>
    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee is null)
            return null;

        return mapper.Map<EmployeeDto>(employee);
    }
}
