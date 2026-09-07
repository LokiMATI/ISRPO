using Microsoft.AspNetCore.Mvc;
using WellnessMassageCenter.Backend.DTO.Employees;
using WellnessMassageCenter.Backend.DTO.Positions;
using WellnessMassageCenter.Backend.DTO.Services;
using WellnessMassageCenter.Backend.Services;

namespace WellnessMassageCenter.Backend.API.Controllers;

/// <summary>
/// Услуги
/// </summary>
/// <param name="service">Сервис услуг</param>
[Route("api/services")]
[ApiController]
public class ServicesController(MassageServicesService service) : ControllerBase
{
    /// <summary>
    /// Получить список услуг
    /// </summary>
    /// <response code="200">Список услуг получен</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet]
    [ProducesResponseType<IEnumerable<ServiceDto>>(200)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetList()
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
    /// Получить информацию об услуге по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор услуги</param>
    /// <response code="200">Услуга получена</response>
    /// <response code="404">Услуги с таким идентификатором не существует</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ServiceDto>(200)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> Get(int id)
    {
        try
        {
            var data = await service.GetByIdAsync(id);
            if (data is null)
                return NotFound("Услуги с таким идентификатором не существует");

            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
