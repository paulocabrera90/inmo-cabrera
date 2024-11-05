using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Inmueble_cabrera.Models.ContratoModels;
using Inmueble_cabrera.Repository;
using Microsoft.EntityFrameworkCore;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ContratosController : ControllerBase
{
    private readonly IContratosRepository _repository;

    public ContratosController(IContratosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContratos()
    {
        var contratos = await _repository.GetAllContratosAsync();
        return Ok(contratos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContrato(int id)
    {
        var contrato = await _repository.GetContratoByIdAsync(id);
        if (contrato == null)
        {
            return NotFound();
        }
        return Ok(contrato);
    }

    [HttpGet("vigentes")]
    public async Task<IActionResult> GetContratoByIdVigentesAsync([FromQuery] bool flagVigente)
    {        

        int idPropietario = Convert.ToInt32(User.FindFirst("Id_propietario")?.Value);
        var contratos = await _repository.GetContratoByIdVigentesAsync(idPropietario, flagVigente);
        if (contratos == null)
        {
            return NotFound();
        }
        return Ok(contratos);
    }
   
}
