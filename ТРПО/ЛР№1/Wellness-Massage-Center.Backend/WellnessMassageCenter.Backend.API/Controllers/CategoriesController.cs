using Microsoft.AspNetCore.Mvc;
using WellnessMassageCenter.Backend.DTO.Categories;
using WellnessMassageCenter.Backend.Services;

namespace WellnessMassageCenter.Backend.API.Controllers;

/// <summary>
/// Категории услуг
/// </summary>
/// <param name="service">Сервис категорий</param>
[Route("api/categories")]
[ApiController]
public class CategoriesController(CategoriesService service) : ControllerBase
{
    /// <summary>
    /// Получить список категорий
    /// </summary>
    /// <param name="isIncludeSubobjects">Включить ли подгрузку подобъектов</param>
    /// <response code="200">Список категорий получен</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet]
    [ProducesResponseType<IEnumerable<CategoryDto>>(200)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetList([FromQuery] bool isIncludeSubobjects = false)
    {
        try
        {
            var data = await service.GetListAsync(isIncludeSubobjects);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Получить категорию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="isIncludeSubobjects">Включить ли подгрузку подобъектов</param>
    /// <response code="200">Категория получена</response>
    /// <response code="404">Категории с таким идентификатором не существует</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType<CategoryDto>(200)]
    public async Task<ActionResult<CategoryDto>> Get(int id, [FromQuery] bool isIncludeSubobjects = false)
    {
        try
        {
            var data = await service.GetByIdAsync(id, isIncludeSubobjects);
            if (data is null)
                return NotFound("Категории с таким идентификатором не существует");

            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
