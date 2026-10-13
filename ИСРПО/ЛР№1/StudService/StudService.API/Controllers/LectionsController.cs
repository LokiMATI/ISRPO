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
public class LectionsController(StudDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var lections = await context.Lections.AsNoTracking().ToListAsync();
            return Ok(lections);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var lection = await context.Lections.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            if (lection is null)
                return NotFound();

            return Ok(lection);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Учитель")]
    public async Task<IActionResult> Create([FromBody] CreateLectionDto input)
    {
        try
        {
            if (await context.Lections.AnyAsync(l => l.Number == input.Number
            && l.CourseId == input.CourseId))
                return BadRequest("Лекция у данного курса с таким порядковым номером уже существует.");

            Lection lection = new()
            {
                Title = input.Title,
                Content = input.Content,
                CourseId = input.CourseId,
                Number = input.Number,
                Image = input.Image
            };

            await context.Lections.AddAsync(lection);
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
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLectionDto input)
    {
        try
        {
            var lection = await context.Lections.FirstOrDefaultAsync(l => l.Id == id);
            if (lection is null)
                return NotFound();

            if (lection.Number != input.Number)
            {
                var tempLection = await context.Lections.FirstOrDefaultAsync(l => l.CourseId == lection.CourseId && l.Number == input.Number);
                if (tempLection is not null)
                {
                    var tempNumber = tempLection.Number;
                    tempLection.Number = input.Number;
                    lection.Number = tempNumber;
                }
            }
            lection.Title = input.Title;
            lection.Content = input.Content;
            lection.Image = input.Image;

            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Учитель")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var lection = await context.Lections.FirstOrDefaultAsync(l => l.Id == id);
            if (lection is null)
                return NotFound();

            context.Lections.Remove(lection);
            await context.SaveChangesAsync();

            await context.Lections.Where(l => l.CourseId == lection.CourseId && l.Number > lection.Number)
                .ExecuteUpdateAsync(setters => setters.SetProperty(l => l.Number, l => l.Number - 1));
        }
        catch (Exception)
        {
            return StatusCode(500);
        }

        return NoContent();
    }
}
