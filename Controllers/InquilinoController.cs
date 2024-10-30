using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Inmueble_cabrera.Repository;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class InquilinosController : ControllerBase
{
    private readonly IInquilinoRepository _repository;

    public InquilinosController(IInquilinoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetInquilinos()
    {
        var inquilinos = await _repository.GetInquilinosAsync();
        return Ok(inquilinos);
    }

    [HttpGet("by-inmueble/{idInmueble}")]
    public async Task<IActionResult> GetInquilinoByImueble(int idInmueble)
    {
        var inquilinos = await _repository.GetInquilinoByInmuebleAsync(idInmueble);
        return Ok(inquilinos);
    }

}
