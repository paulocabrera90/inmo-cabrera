using Microsoft.AspNetCore.Mvc;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Inmueble_cabrera.Services;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class TiposController : ControllerBase
{
    private readonly ITiposRepository _repository;

    public TiposController(ITiposRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("tipos-inmueble")]
    public async Task<IActionResult> GetTiposInmueble()
    {
        var tiposInmueble = await _repository.GetTiposInmueblesAsync();
        return Ok(tiposInmueble);
    }

    [HttpGet("tipos-inmueble-uso")]
    public async Task<IActionResult> GetTiposInmuebleUso()
    {
        var tiposInmuebleUso = await _repository.GetTiposInmueblesUsoAsync();
        return Ok(tiposInmuebleUso);
    }
}
