using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            .Include(c => c.IdInquilino)
            .Include(c => c.IdInmueble)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contrato> CreateContratoAsync(Contrato contrato)
    {
        contrato.FechaCreacion = DateTime.Today;
        contrato.FechaActualizacion = DateTime.Today;

        _context.Contratos.Add(contrato);
        await _context.SaveChangesAsync();
        return contrato;
    }

    public async Task UpdateContratoAsync(Contrato contrato)
    {
        contrato.FechaActualizacion = DateTime.Today;

        _context.Attach(contrato).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContratoAsync(int id)
    {
        var contrato = await _context.Contratos.FindAsync(id);
        if (contrato != null)
        {
            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();
        }
    }

    public bool ContratoExists(int id)
    {
        return _context.Contratos.Any(c => c.Id == id);
    }

    // Task<Contrato> UpdateContratoAsync(Contrato contrato)
    // {
    //     throw new NotImplementedException();
    // }

    // Task<bool> IContratosRepository.ContratoExists(int id)
    // {
    //     return _context.Contratos.Any(c => c.Id == id);
    // }
}