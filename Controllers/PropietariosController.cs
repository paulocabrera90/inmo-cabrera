using Microsoft.AspNetCore.Mvc;
using InmobiliariaCA.Models;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.JsonPatch;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class PropietariosController : ControllerBase
{
    private readonly IPropietariosRepository _repository;

    public PropietariosController(IPropietariosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPropietarios()
    {
        var propietarios = await _repository.GetAllPropietariosAsync();
        return Ok(propietarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropietario(int id)
    {
        var propietario = await _repository.GetPropietarioByIdAsync(id);
        if (propietario == null)
        {
            return NotFound();
        }
        return Ok(PropietarioMapper.ToMapper(propietario));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePropietario([FromBody] Propietario propietario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPropietario = await _repository.CreatePropietarioAsync(propietario);
        return CreatedAtAction(nameof(GetPropietario), new { id = createdPropietario.Id }, createdPropietario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePropietario(int id, [FromBody] Propietario propietario)
    {
        var existingPropietario = await _repository.GetPropietarioByIdAsync(id);
        if (existingPropietario == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(existingPropietario))
        {
            return BadRequest(ModelState);
        }

        try
        {
            existingPropietario = await _repository.ApplyChanges(existingPropietario, propietario);
            await _repository.UpdatePropietarioAsync(existingPropietario);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_repository.PropietarioExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }


        return Ok(PropietarioMapper.ToMapper(existingPropietario));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropietario(int id)
    {
        if (!_repository.PropietarioExists(id))
        {
            return NotFound();
        }

        await _repository.DeletePropietarioAsync(id);
        return NoContent();
    }
}