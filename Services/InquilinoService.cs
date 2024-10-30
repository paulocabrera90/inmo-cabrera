using Inmueble_cabrera.Data;
using Inmueble_cabrera.Models;
using Inmueble_cabrera.Repository;
using Microsoft.EntityFrameworkCore;
namespace Inmueble_cabrera.Services;

public class InquilinoService : IInquilinoRepository
{
    private readonly DataContext _context;

    public InquilinoService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inquilino>> GetInquilinosAsync()
    {
       return await _context.Inquilinos.ToListAsync();
    }
}