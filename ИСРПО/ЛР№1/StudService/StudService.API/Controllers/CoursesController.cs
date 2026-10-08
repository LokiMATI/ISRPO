using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudService.DTO;
using StudService.EntityFramework.Contexts;
using StudService.Models;

namespace StudService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CoursesController(StudDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList()
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

	[HttpPost]
	[Authorize(Roles = "Учитель")]
	public async Task<IActionResult> Create([FromBody] CreateAndUpdateCourseDto input)
	{
		try
		{
			Course course = new()
			{
				Title = input.Title,
				Description = input.Description,
				Image = input.Image
			};

			await context.Courses.AddAsync(course);
			await context.SaveChangesAsync();
		}
		catch (Exception)
		{
			return StatusCode(500);
		}

		return Ok();
	}

    [HttpPut("{id:int}")]
	[Authorize(Roles = "Учитель")]
	public async Task<IActionResult> Update(int id, [FromBody] CreateAndUpdateCourseDto input)
	{
		try
		{
			var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
			if (course is null)
				return NotFound();

			course.Title = input.Title;
			course.Description = input.Description;
			course.Image = input.Image;

			context.Courses.Update(course);
			await context.SaveChangesAsync();
		}
		catch(DbUpdateException)
		{
			return BadRequest();
		}
		catch (Exception)
		{
            return StatusCode(500);
        }
		return Ok();
	}
}
