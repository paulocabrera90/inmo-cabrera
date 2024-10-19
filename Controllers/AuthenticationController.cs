using InmobiliariaCA.Models;
using InmobiliariaCA.Services;
using Inmueble_cabrera.Data;
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
            var user = await context.Propietarios.FirstOrDefaultAsync(u => u.Usuario == loginView.Usuario);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginView.Password, user.Password))
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

}