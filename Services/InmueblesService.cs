using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Repository;
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
        .Include(i => i.Propietario)
        .ToListAsync();
    }


    public async Task<Inmueble> GetInmuebleByIdAsync(int id)
    {
        return await _context.Inmuebles
            .Include(i => i.Tipo)
            .Include(i => i.TipoUso)
            .Include(i => i.Propietario)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Inmueble> CreateInmuebleAsync(Inmueble inmueble, IFormFile? image)
    {
        inmueble.FechaActualizacion = DateTime.Today;
        inmueble.FechaCreacion = DateTime.Today;

        if (image != null && image.Length > 0)
        {
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                inmueble.ImageBlob = stream.ToArray();
            }
        }

        _context.Inmuebles.Add(inmueble);
        await _context.SaveChangesAsync();
        return inmueble;
    }

    public async Task UpdateInmuebleAsync(Inmueble inmueble, IFormFile? image)
    {
        inmueble.FechaActualizacion = DateTime.Today;

        if (image != null && image.Length > 0)
        {
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                inmueble.ImageBlob = stream.ToArray();
            }
        }
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

    public async Task<Inmueble> ApplyChanges(Inmueble existingInmueble, Inmueble inmueble)
    {
        // Aplicar solo los campos modificados (si no son null o valores vacíos)
        if (!string.IsNullOrEmpty(inmueble.Direccion))
            existingInmueble.Direccion = inmueble.Direccion;

        if (!string.IsNullOrEmpty(inmueble.IdPropietario.ToString()))
            existingInmueble.IdPropietario = inmueble.IdPropietario;

        if (!string.IsNullOrEmpty(inmueble.IdTipoInmueble.ToString()))
            existingInmueble.IdTipoInmueble = inmueble.IdTipoInmueble;

        if (!string.IsNullOrEmpty(inmueble.IdTipoInmuebleUso.ToString()))
            existingInmueble.IdTipoInmuebleUso = inmueble.IdTipoInmuebleUso;

        if (!string.IsNullOrEmpty(inmueble.Direccion))
            existingInmueble.Direccion = inmueble.Direccion;

        if (!string.IsNullOrEmpty(inmueble.Ambientes.ToString()))
            existingInmueble.Ambientes = inmueble.Ambientes;

        if (!string.IsNullOrEmpty(inmueble.Precio.ToString()))
            existingInmueble.Precio = inmueble.Precio;

        if (!string.IsNullOrEmpty(inmueble.CoordenadaLat))
            existingInmueble.CoordenadaLat = inmueble.CoordenadaLat;

        if (!string.IsNullOrEmpty(inmueble.CoordenadaLon))
            existingInmueble.CoordenadaLon = inmueble.CoordenadaLon;

        if (!string.IsNullOrEmpty(inmueble.Activo.ToString()))
            existingInmueble.Activo = inmueble.Activo;

        //IMAGEN   

        // Actualizar la fecha de modificación
        existingInmueble.FechaActualizacion = DateTime.Now;


        return existingInmueble;
    }

    public async Task<IEnumerable<Inmueble>> GetInmueblesByPropConContratosAsync()
    {
        return await _context.Contratos
            .Where(c => c.Estado == EstadoContrato.Vigente)
            .Include(c => c.Inmueble.Tipo)
            .Include(c => c.Inmueble.TipoUso)
            .Select(c => c.Inmueble)
            .Distinct()
            .ToListAsync();

    }
    public async Task<IEnumerable<Inmueble>> GetAllInmueblesByPropietarioIdAsync(int id)
    {
        return await _context.Inmuebles
       .Include(i => i.Tipo)
       .Include(i => i.TipoUso)
       .Where(i => i.IdPropietario == id)
       .ToListAsync();
    }

    public async Task<byte[]?> GetArchivoByInmuebleIdAsync(int id)
    {
        var inmueble = await _context.Inmuebles
            .Where(i => i.Id == id)
            .Select(i => i.ImageBlob)
            .FirstOrDefaultAsync();

        return inmueble;
    }


}