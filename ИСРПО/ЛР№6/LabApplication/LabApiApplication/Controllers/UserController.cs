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
				throw new FormatException();

			int value = int.Parse(id);

			if (value > _users.Count() || value < 0)
				throw new NotFoundResult();

			return Ok(_users[value]);
		}
		catch (FormatException)
		{
			return BadRequest(new ErrorMessage() { Error = "Invalid ID format", StatusCode = 400 });
		}
		catch (Exception ex)
		{
            return BadRequest(new ErrorMessage() { Error = ex.Message, StatusCode = 400 });
        }
    }
}
