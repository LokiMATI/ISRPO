using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudService.DTO;
using StudService.EntityFramework.Contexts;

namespace StudService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var lection = await context.Lections.FirstOrDefaultAsync(l => l.Id == id);
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
    public void Create([FromBody] CreateLectionDto input)
    {
        try
        {
            if 
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    // PUT api/<LectionsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<LectionsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
