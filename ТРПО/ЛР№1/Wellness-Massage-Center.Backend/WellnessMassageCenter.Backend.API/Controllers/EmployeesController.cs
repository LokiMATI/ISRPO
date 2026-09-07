using Microsoft.AspNetCore.Mvc;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.Services;

namespace WellnessMassageCenter.Backend.API.Controllers;

/// <summary>
/// Сотрудники
/// </summary>
[Route("api/employees")]
[ApiController]
public class EmployeesController(EmployeesService service) : ControllerBase
{
    /// <summary>
    /// Получить список сотрудников
    /// </summary>
    /// <response code="200">Список сотрудников получен</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet]
    [ProducesResponseType<IEnumerable<EmployeeDto>>(200)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetList()
    {
		try
		{
            var data = await service.GetListAsync();
            return Ok(data);
        }
		catch (Exception ex)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получить сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <response code="200">Сотрудник получен</response>
    /// <response code="404">Сотрудника с таким идентификатором не существует</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<EmployeeDto>(200)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get(int id)
    {
        try
        {
            var data = await service.GetByIdAsync(id);
            if (data is null)
                return NotFound("Сотрудника с таким идентификатором не существует");

            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
