using Microsoft.AspNetCore.Mvc;

namespace LabApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private List<string> _users = ["Test", "Lucky", "Nikita"];
    [HttpGet]
    public IActionResult GetById(string id)
    {
		try
		{
			if (id.Any(c => !char.IsDigit(c)))
				throw new FormatException("Invalid ID format.");

			int value = int.Parse(id);

			if (value > _users.Count() || value < 0)
				throw new KeyNotFoundException("Пользователь с таким идентификатором не был найден.");

			return Ok(_users[value]);
		}
		catch (KeyNotFoundException ex)
        {
            LogException(ex);
            return NotFound(new ErrorMessage() { Error = ex.Message, StatusCode = 404 });
        }
        catch (FormatException ex)
		{
            LogException(ex);
            return BadRequest(new ErrorMessage() { Error = "Invalid ID format", StatusCode = 400 });
		}
		catch (Exception ex)
		{
            LogException(ex);
            return BadRequest(new ErrorMessage() { Error = ex.Message, StatusCode = 400 });
        }
    }

    private void LogException(Exception ex)
    {
        System.IO.File.AppendAllText("log.txt", $"[{DateTime.Now}] {HttpContext.Request.HttpContext.Connection.RemoteIpAddress}: {ex.Message}\n");
    }
}
