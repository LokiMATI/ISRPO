using Microsoft.AspNetCore.Mvc;
using WellnessMassageCenter.Backend.DAL.Models;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.Services;

namespace WellnessMassageCenter.Backend.API.Controllers;

/// <summary>
/// Должности
/// </summary>
/// <param name="service">Сервис должности</param>
[Route("api/positions")]
[ApiController]
public class PositionsController(PositionsService service) : ControllerBase
{
    /// <summary>
    /// Получить список должностей
    /// </summary>
    /// <response code="200">Список должностей получен</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet]
    [ProducesResponseType<IEnumerable<PositionDto>>(200)]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetList()
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
    /// Получить должность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор должности</param>
    /// <response code="200">Должность получена</response>
    /// <response code="404">Должности с таким идентификатором не существует</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<PositionDto>(200)]
    public async Task<ActionResult<IEnumerable<PositionDto>>> Get(int id)
    {
        try
        {
            var data = await service.GetByIdAsync(id);
            if (data is null)
                return NotFound("Должности с таким идентификатором не существует");

            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
