using InmobiliariaCA.Models;
using Inmueble_cabrera.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Inmueble_cabrera.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropietariosController : ControllerBase
{
	private readonly DataContext contexto;
	private readonly IConfiguration config;
	private readonly IWebHostEnvironment environment;

	public PropietariosController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
	{
		this.contexto = contexto;
		this.config = config;
		environment = env;
	}

	[HttpGet]
	//[SwaggerOperation(Summary = "Retrieves the current weather forecast")]
	public async Task<IActionResult> GetPropietarios()
	{
		var propietarios = await contexto.Propietarios.ToListAsync();
		return Ok(propietarios);
	}

	[HttpGet("{id}")]
	//[SwaggerOperation(Summary = "Retrieves the current weather forecast")]
	public async Task<IActionResult> GetPropietario(int id)
	{
		var propietario = await contexto.Propietarios.FirstOrDefaultAsync(p => p.Id == id);

		if (propietario == null)
		{
			return NotFound();
		}

		return Ok(propietario);
	}

	[HttpPost]
	//[SwaggerOperation(Summary = "Crea un nuevo propietario")]
	public async Task<IActionResult> CreatePropietario([FromBody] Propietario propietario)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		contexto.Propietarios.Add(propietario);
		await contexto.SaveChangesAsync();

		return CreatedAtAction(nameof(GetPropietario), new { id = propietario.Id }, propietario);
	}

	[HttpPut("{id}")]
	//[SwaggerOperation(Summary = "Actualiza un propietario existente")]
	public async Task<IActionResult> UpdatePropietario(int id, [FromBody] Propietario propietario)
	{
		if (id != propietario.Id)
		{
			return BadRequest("ID de propietario no coincide");
		}

		if (!contexto.Propietarios.Any(e => e.Id == id))
		{
			return NotFound();
		}

		contexto.Entry(propietario).State = EntityState.Modified;

		try
		{
			await contexto.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!PropietarioExists(id))
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
	//[SwaggerOperation(Summary = "Elimina un propietario existente")]
	public async Task<IActionResult> DeletePropietario(int id)
	{
		var propietario = await contexto.Propietarios.FindAsync(id);
		if (propietario == null)
		{
			return NotFound();
		}

		contexto.Propietarios.Remove(propietario);
		await contexto.SaveChangesAsync();

		return NoContent();
	}

	private bool PropietarioExists(int id)
	{
		return contexto.Propietarios.Any(e => e.Id == id);
	}

}