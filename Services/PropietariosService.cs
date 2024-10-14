using InmobiliariaCA.Models;
using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;

namespace Inmueble_cabrera.Services;
public class PropietariosService : IPropietariosService
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
        _context.Entry(propietario).State = EntityState.Modified;
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
}
