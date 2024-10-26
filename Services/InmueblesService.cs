using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
namespace Inmueble_cabrera.Services;

public class InmueblesService : IInmueblesRepository
{
    private readonly DataContext _context;

    public InmueblesService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inmueble>> GetAllInmueblesAsync()
    {
        return await _context.Inmuebles
        .Include(i => i.Tipo)
        .Include(i => i.TipoUso)
        .ToListAsync();
    }


    public async Task<Inmueble> GetInmuebleByIdAsync(int id)
    {
        return await _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.TipoUso)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Inmueble> CreateInmuebleAsync(Inmueble inmueble)
    {
        _context.Inmuebles.Add(inmueble);
        await _context.SaveChangesAsync();
        return inmueble;
    }

    public async Task UpdateInmuebleAsync(Inmueble inmueble)
    {
        _context.Attach(inmueble).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteInmuebleAsync(int id)
    {
        var inmueble = await _context.Inmuebles.FindAsync(id);
        if (inmueble != null)
        {
            _context.Inmuebles.Remove(inmueble);
            await _context.SaveChangesAsync();
        }
    }

    public bool InmuebleExists(int id)
    {
        return _context.Inmuebles.Any(i => i.Id == id);
    }

    public async Task<Inmueble> ApplyChanges(Inmueble existingInmueble, Inmueble inmuebleDto)
    {
        // Implementar lógica para aplicar cambios específicos del DTO al inmueble existente, similar a Propietarios
        existingInmueble.Direccion = inmuebleDto.Direccion ?? existingInmueble.Direccion;
        existingInmueble.CoordenadaLat = inmuebleDto.CoordenadaLat ?? existingInmueble.CoordenadaLat;
        existingInmueble.CoordenadaLon = inmuebleDto.CoordenadaLon ?? existingInmueble.CoordenadaLon;
        existingInmueble.Precio = inmuebleDto.Precio != default ? inmuebleDto.Precio : existingInmueble.Precio;
        existingInmueble.FechaActualizacion = DateTime.Now;

        // Asegúrate de incluir lógica para actualizar relaciones como Tipo y Tipo_Uso si es necesario

        return existingInmueble;
    }

    public async Task<IEnumerable<Inmueble>>  GetAllInmueblesByPropietarioIdAsync(int id)
    {
         return await _context.Inmuebles
        .Include(i => i.Tipo)
        .Include(i => i.TipoUso)
        .Where(i => i.IdPropietario == id) // Filter by Id_Propietario
        .ToListAsync();
    }

}