using InmobiliariaCA.Models;
using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Inmueble_cabrera.Services;
public class PropietariosService : IPropietariosRepository
{
    private readonly DataContext _context;

    public PropietariosService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Propietario>> GetAllPropietariosAsync()
    {
        return await _context.Propietarios.ToListAsync();
    }

    public async Task<Propietario> GetPropietarioByIdAsync(int id)
    {
        return await _context.Propietarios.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Propietario> CreatePropietarioAsync(Propietario propietario)
    {
        propietario.Password = Commons.CreatePasswordHash(propietario.Password);
        _context.Propietarios.Add(propietario);
        await _context.SaveChangesAsync();
        return propietario;
    }

    public async Task UpdatePropietarioAsync(Propietario propietario)
    {

        {
            _context.Attach(propietario);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeletePropietarioAsync(int id)
    {
        var propietario = await _context.Propietarios.FindAsync(id);
        if (propietario != null)
        {
            _context.Propietarios.Remove(propietario);
            await _context.SaveChangesAsync();
        }
    }

    public bool PropietarioExists(int id)
    {
        return _context.Propietarios.Any(e => e.Id == id);
    }

    public async Task<Propietario> ApplyChanges(Propietario existingPropietario, Propietario propietarioDto)
    {
        // Aplicar solo los campos modificados (si no son null o valores vacíos)
        if (!string.IsNullOrEmpty(propietarioDto.Dni))
            existingPropietario.Dni = propietarioDto.Dni;

        if (!string.IsNullOrEmpty(propietarioDto.Nombre))
            existingPropietario.Nombre = propietarioDto.Nombre;

        if (!string.IsNullOrEmpty(propietarioDto.Apellido))
            existingPropietario.Apellido = propietarioDto.Apellido;

        if (!string.IsNullOrEmpty(propietarioDto.Email))
            existingPropietario.Email = propietarioDto.Email;

        if (!string.IsNullOrEmpty(propietarioDto.Direccion))
            existingPropietario.Direccion = propietarioDto.Direccion;

        if (!string.IsNullOrEmpty(propietarioDto.Telefono_Area))
            existingPropietario.Telefono_Area = propietarioDto.Telefono_Area;

        if (!string.IsNullOrEmpty(propietarioDto.Telefono_Numero))
            existingPropietario.Telefono_Numero = propietarioDto.Telefono_Numero;

        // Actualizar la fecha de modificación
        existingPropietario.Fecha_Actualizacion = DateTime.Now;

        return existingPropietario;
    }

}
