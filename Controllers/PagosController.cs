using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Repository;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PagosController : ControllerBase
{
    private readonly IPagosRepository _repository;

    public PagosController(IPagosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPagos()
    {
        int id = Convert.ToInt32(User.FindFirst("Id_propietario")?.Value);

        var pagos = await _repository.GetAllPagosAsync();
        return Ok(pagos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPago(int id)
    {
        var pago = await _repository.GetPagoByIdAsync(id);
        if (pago == null)
        {
            return NotFound();
        }
        return Ok(pago);
    }

     [HttpGet("by-contrato/{id}")]
    public async Task<IActionResult> GetPagoByContratoId(int id)
    {
        var pago = await _repository.GetPagoByContratoIdAsync(id);
        if (pago == null)
        {
            return NotFound();
        }
        return Ok(pago);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePago([FromBody] Pago pago)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdPago = await _repository.CreatePagoAsync(pago);
        return CreatedAtAction(nameof(GetPago), new { id = createdPago.Id }, createdPago);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePago(int id, [FromBody] Pago pago)
    {
        var existingPago = await _repository.GetPagoByIdAsync(pago.Id);
         if (existingPago == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != pago.Id)
        {
            return BadRequest("ID mismatch");
        }

         try
        {
            // existingPago = await _repository.ApplyChanges(existingPago, pago);
            await _repository.UpdatePagoAsync(existingPago);;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_repository.PagoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(existingPago);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePago(int id)
    {
        if (!_repository.PagoExists(id))
        {
            return NotFound();
        }

        await _repository.DeletePagoAsync(id);
        return NoContent();
    }
}
