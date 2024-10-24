using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Models;
public interface IInmueblesRepository
{
    Task<IEnumerable<Inmueble>> GetAllInmueblesAsync();
    Task<IEnumerable<Inmueble>> GetAllInmueblesByPropietarioIdAsync(int id);
    Task<Inmueble> GetInmuebleByIdAsync(int id);
    Task<Inmueble> CreateInmuebleAsync(Inmueble inmueble);
    Task UpdateInmuebleAsync(Inmueble inmueble);
    Task DeleteInmuebleAsync(int id);
    bool InmuebleExists(int id);
}
