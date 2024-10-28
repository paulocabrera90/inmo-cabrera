using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Microsoft.EntityFrameworkCore;
namespace Inmueble_cabrera.Services;

public class TiposService : ITiposRepository
{
    private readonly DataContext _context;

    public TiposService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoInmueble>> GetTiposInmueblesAsync()
    {
       return await _context.Tipos_Inmueble.ToListAsync();
    }

    public async Task<IEnumerable<TipoInmuebleUso>> GetTiposInmueblesUsoAsync()
    {
       return await _context.Tipos_Inmueble_Uso.ToListAsync();
    }
}