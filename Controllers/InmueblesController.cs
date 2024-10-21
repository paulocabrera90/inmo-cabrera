using Microsoft.AspNetCore.Mvc;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InmueblesController : ControllerBase
{
    private readonly IInmueblesRepository _repository;

    public InmueblesController(IInmueblesRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetInmuebles()
    {
        var inmuebles = await _repository.GetAllInmueblesAsync();
        return Ok(inmuebles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInmueble(int id)
    {
        var inmueble = await _repository.GetInmuebleByIdAsync(id);
        if (inmueble == null)
        {
            return NotFound();
        }
        return Ok(inmueble);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInmueble([FromBody] Inmueble inmueble)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdInmueble = await _repository.CreateInmuebleAsync(inmueble);
        return CreatedAtAction(nameof(GetInmueble), new { id = createdInmueble.Id }, createdInmueble);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInmueble(int id, [FromBody] Inmueble inmueble)
    {
        if (id != inmueble.Id)
        {
            return BadRequest("ID mismatch");
        }

        if (!_repository.InmuebleExists(id))
        {
            return NotFound();
        }

        try
        {
            await _repository.UpdateInmuebleAsync(inmueble);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_repository.InmuebleExists(id))
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
    public async Task<IActionResult> DeleteInmueble(int id)
    {
        if (!_repository.InmuebleExists(id))
        {
            return NotFound();
        }

        await _repository.DeleteInmuebleAsync(id);
        return NoContent();
    }
}
