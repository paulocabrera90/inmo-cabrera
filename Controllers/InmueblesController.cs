using Microsoft.AspNetCore.Mvc;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpGet("by-propietario/{id}")]
    public async Task<IActionResult> GetInmueblesByPropietarioId(int id)
    {
        var inmuebles = await _repository.GetAllInmueblesByPropietarioIdAsync(id);
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
    public async Task<IActionResult> CreateInmueble([FromForm] Inmueble inmueble, [FromForm] IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Console.WriteLine("IMAGE", image);
        var createdInmueble = await _repository.CreateInmuebleAsync(inmueble, image);
        return CreatedAtAction(nameof(GetInmueble), new { id = createdInmueble.Id }, createdInmueble);
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

    [HttpPut]
    public async Task<IActionResult> UpdateInmueble([FromForm] Inmueble inmueble, [FromForm] IFormFile? image)
    {
        //CONTROLAR EL ID DEL PROPIETARIO
        int id = Convert.ToInt32(User.FindFirst("Id_propietario")?.Value);

        if (id != inmueble.IdPropietario)
        {
            return BadRequest("No puede editar el inmueble, ya que no es propietario");
        }

        var existingInmueble = await _repository.GetInmuebleByIdAsync(inmueble.Id);
        if (existingInmueble == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(existingInmueble))
        {
            return BadRequest(ModelState);
        }

        try
        {
            existingInmueble = await _repository.ApplyChanges(existingInmueble, inmueble);
            await _repository.UpdateInmuebleAsync(existingInmueble, image);;
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


        return Ok(existingInmueble);
    }

    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetArchivo(int id)
    {
        var archivoBase64 = await _repository.GetArchivoByInmuebleIdAsync(id);
        if (archivoBase64 == null)
        {
            return NotFound(new { mensaje = "No se encuentra archivo o imagen para el inmueble especificado." });
        }

       // return Ok(new { archivoBase64 });
       return  Ok(File(archivoBase64, "image/jpeg"));
    }
}
