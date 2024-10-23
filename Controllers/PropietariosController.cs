using Microsoft.AspNetCore.Mvc;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Identity.Data;
using Inmueble_cabrera.Models.Authentication;

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
    public async Task<IActionResult> UpdatePropietario([FromBody] Propietario propietario)

    {
        int id = Convert.ToInt32(User.FindFirst("Id_propietario")?.Value);

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

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var success = await _repository.ResetPassword(request.Email);
        if (!success)
        {
            return NotFound(new { message = "Password reset error. No user found with that email or not a user role." });
        }

        return Ok(new { message = "Password reset successful. Please check your email for the new password." });
    }

    [HttpPost("validate-code")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateVerificationNumber([FromBody] VerificationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var statusVerification = await _repository.VerifyNumberStatusAsync(request.VerificationNumber, request.Email);
            if (statusVerification == null)
            {
                return NotFound(new { message = "Verification failed. Details not found." });
            }

            return Ok(statusVerification);
        }
        catch (Exception ex)
        {
            // Logging the exception might be useful
            return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        }
    }

}
