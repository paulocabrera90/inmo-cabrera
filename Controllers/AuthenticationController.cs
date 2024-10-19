using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models.Authentication;
using Inmueble_cabrera.Services;
using Inmueble_cabrera.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly DataContext context;
    private readonly IConfiguration config;
    private readonly IWebHostEnvironment environment;

    public AuthenticationController(DataContext context, IConfiguration config, IWebHostEnvironment env)
    {
        this.context = context;
        this.config = config;
        environment = env;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginView loginView)
    {
        try
        {
            var user = await context.Propietarios.FirstOrDefaultAsync(u => u.Email == loginView.Email);
            if (user == null || !Commons.VerifyPassword(loginView.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            var tokenService = new TokenService(context, config, environment);
            var token = tokenService.GenerateJwtToken(user.Email);

            var result = new
            {
                data = new
                {
                    id = user.Id,
                    nombre = user.Nombre,
                    apellido = user.Apellido,
                    email = user.Email
                },
                tokenSession = token
            };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordView changePasswordView)
    {
        try
        {
            var user = await context.Propietarios.FirstOrDefaultAsync(u => u.Email == changePasswordView.Email && u.Id == changePasswordView.Id);

            if (user == null || !Commons.VerifyPassword(changePasswordView.CurrentPassword, user.Password))
            {
                 return Conflict("There is a conflict with the provided data.");
            }           

            user.Password = Commons.CreatePasswordHash(changePasswordView.NewPassword);

            // Guardar los cambios en la base de datos
            context.Propietarios.Update(user);
            await context.SaveChangesAsync();

            return Ok("Password updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}