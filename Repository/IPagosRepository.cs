using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Repository;
public interface IPagosRepository
{
    Task<IEnumerable<Pago>> GetAllPagosAsync();
    
    Task<Pago> GetPagoByIdAsync(int id);

    Task<Pago> CreatePagoAsync(Pago pago);

    Task UpdatePagoAsync(Pago pago);

    Task DeletePagoAsync(int id);

    bool PagoExists(int id);
}
