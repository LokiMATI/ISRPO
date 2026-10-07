using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudService.EntityFramework.Contexts;

namespace StudService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(StudDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
		try
		{
			var courses = await context.Courses.ToListAsync();
			return Ok(courses);
		}
		catch (Exception)
		{
			return StatusCode(500);
		}
    }
}
