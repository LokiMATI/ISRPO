using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudService.EntityFramework.Contexts;

namespace StudService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController(StudDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var tasks = await context.Tasks.AsNoTracking().ToListAsync();
            return Ok(tasks);
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
            var task = await context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
                return NotFound();

            return Ok(task);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
    }

    // PUT api/<ValuesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
