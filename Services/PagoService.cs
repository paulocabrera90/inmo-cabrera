using Inmueble_cabrera.Data;
using Inmueble_cabrera.Repository;
using Microsoft.EntityFrameworkCore;
using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Services;

public class PagosService : IPagosRepository
{
    private readonly DataContext _context;

    public PagosService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pago>> GetAllPagosAsync()
    {

          return await _context.Pagos
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inmueble)
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inquilino)
            .ToListAsync();
    }

    public async Task<Pago> GetPagoByIdAsync(int id)
    {          
        return await _context.Pagos
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inmueble)
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inquilino)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Pago> GetPagoByContratoIdAsync(int idContrato)
    {          
        return await _context.Pagos
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inmueble)
            .Include(p => p.Contrato)
                .ThenInclude(contrato => contrato.Inquilino)
            .FirstOrDefaultAsync(c => c.ContratoId == idContrato);
    }

    public async Task<Pago> CreatePagoAsync(Pago pago)
    {
        pago.FechaCreacion = DateTime.Today;
        pago.FechaActualizacion = DateTime.Today;

        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();
        return pago;
    }

    public async Task UpdatePagoAsync(Pago pago)
    {
        pago.FechaActualizacion = DateTime.Today;

        _context.Attach(pago).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletePagoAsync(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);
        if (pago != null)
        {
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
        }
    }

    public bool PagoExists(int id)
    {
        return _context.Pagos.Any(c => c.Id == id);
    }
}