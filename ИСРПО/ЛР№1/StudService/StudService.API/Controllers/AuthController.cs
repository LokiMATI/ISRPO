using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudService.DTO;
using StudService.EntityFramework.Contexts;
using StudService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudService.API.Controllers;

[Route("api")]
[ApiController]
public class AuthController(IConfiguration configuration, StudDbContext context) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto input)
    {
        try
        {
            var user = await context.Users.AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == input.Login);

            if (user is null)
                return Unauthorized("Неверные данные авторизации.");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto input)
    {
        try
        {
            if (await context.Users.AnyAsync(u => u.Login == input.Login))
                return Conflict("Пользователь с таким логином уже существует.");
            if (await context.Users.AnyAsync(u => u.Email == input.Email))
                return Conflict("Пользователь с такой почтой уже существует.");
            if (input.Phone is not null && await context.Users.AnyAsync(u => u.Phone == input.Phone))
                return Conflict("Пользователь с таким номером телефона уже существует.");

            if (!await context.Roles.AnyAsync(r => r.Id == (int)input.Role))
                return Conflict("Роли с таким идентификаором не существует.");

            User user = new()
            {
                Login = input.Login,
                Email = input.Email,
                RoleId = (int)input.Role,
                Phone = input.Phone
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500);
            throw;
        }

        return Ok();
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.Role.Title)
        };

        JwtSecurityToken token = new(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
