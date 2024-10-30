using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using Inmueble_cabrera.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Inmueble_cabrera.Services;

public class InquilinoService : IInquilinoRepository
{
    private readonly DataContext _context;

    public InquilinoService(DataContext context)
    {
        _context = context;
    }

    public async Task<Inquilino?> GetInquilinoByInmuebleAsync(int idInmueble)
    {
        DateTime currentDate = DateTime.Today;

        var inquilino = await _context.Contratos
            .Include(c => c.Inquilino)
            .Include(c => c.Inmueble)
            .Where(c => c.Estado == Models.EstadoContrato.Vigente
                        && c.FechaHasta >= currentDate
                        && c.IdInmueble == idInmueble
                        && (!c.FechaFinalizacionAnticipada.HasValue || c.FechaFinalizacionAnticipada >= currentDate))
            .Select(c => c.Inquilino)
            .FirstOrDefaultAsync();

        return inquilino;
    }
    public async Task<IEnumerable<Inquilino>> GetInquilinosAsync()
    {
        return await _context.Inquilinos.ToListAsync();
    }
}