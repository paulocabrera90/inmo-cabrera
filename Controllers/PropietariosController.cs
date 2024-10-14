using Microsoft.AspNetCore.Mvc;
using InmobiliariaCA.Models;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropietariosController : ControllerBase
{
    private readonly IPropietariosService _service;

    public PropietariosController(IPropietariosService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPropietarios()
    {
        var propietarios = await _service.GetAllPropietariosAsync();
        return Ok(propietarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropietario(int id)
    {
        var propietario = await _service.GetPropietarioByIdAsync(id);
        if (propietario == null)
        {
            return NotFound();
        }
        return Ok(propietario);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePropietario([FromBody] Propietario propietario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPropietario = await _service.CreatePropietarioAsync(propietario);
        return CreatedAtAction(nameof(GetPropietario), new { id = createdPropietario.Id }, createdPropietario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePropietario(int id, [FromBody] Propietario propietario)
    {
        if (id != propietario.Id)
        {
            return BadRequest("ID mismatch");
        }

        if (!_service.PropietarioExists(id))
        {
            return NotFound();
        }

        try
        {
            await _service.UpdatePropietarioAsync(propietario);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_service.PropietarioExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropietario(int id)
    {
        if (!_service.PropietarioExists(id))
        {
            return NotFound();
        }

        await _service.DeletePropietarioAsync(id);
        return NoContent();
    }
}
