using Inmueble_cabrera.Data;
using Inmueble_cabrera.Repository;
using Inmueble_cabrera.Models.ContratoModels;
using Microsoft.EntityFrameworkCore;

namespace Inmueble_cabrera.Services;

public class ContratosService : IContratosRepository
{
    private readonly DataContext _context;

    public ContratosService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contrato>> GetAllContratosAsync()
    {
        return await _context.Contratos
            .Include(c => c.Inquilino)
            .Include(c => c.Inmueble)
            .ToListAsync();
    }

    public async Task<Contrato> GetContratoByIdAsync(int id)
    {
        return await _context.Contratos
             .Include(c => c.Inquilino)
            .Include(c => c.Inmueble)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Contrato>> GetContratoByIdVigentesAsync(int idPropietario, bool flagVigente)
    {

        IQueryable<Contrato> query = _context.Contratos
        .Include(c => c.Inquilino)
        .Include(c => c.Inmueble)
        .Where(c => c.Inmueble.IdPropietario == idPropietario);

        if (flagVigente)
        {
            DateTime currentDate = DateTime.Today;
            query = query.Where(
                c => c.Estado == Models.EstadoContrato.Vigente 
                    && c.FechaHasta >= currentDate 
                    && (!c.FechaFinalizacionAnticipada.HasValue || c.FechaFinalizacionAnticipada >= currentDate));
        }

        return await query.ToListAsync();
    }

    public bool ContratoExists(int id)
    {
        return _context.Contratos.Any(c => c.Id == id);
    }
}