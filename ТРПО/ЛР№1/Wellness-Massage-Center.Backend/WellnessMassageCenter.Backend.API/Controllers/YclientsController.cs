using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WellnessMassageCenter.Backend.Services;

namespace WellnessMassageCenter.Backend.API.Controllers;

/// <summary>
/// Работы с Yclients
/// </summary>
/// <param name="service">Сервис Yclients</param>
[Route("api/yclients")]
[ApiController]
public class YclientsController(YclientsService service) : ControllerBase
{
    /// <summary>
    /// Синхронизовать данные базы данных сайта с Yclients
    /// </summary>
    /// <response code="200">Синхронизация завершена успешно</response>
    /// <response code="500">Возникла ошибка на стороне сервера</response>
    [HttpPost("synchronization")]
    public async Task<IActionResult> SynchronizeData()
    {
        try
        {
            await service.SynchronizeDataAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
