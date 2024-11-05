using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Repository;
public interface IInmueblesRepository
{
    Task<IEnumerable<Inmueble>> GetAllInmueblesAsync();
    
    Task<IEnumerable<Inmueble>> GetAllInmueblesByPropietarioIdAsync(int id);

    Task<IEnumerable<Inmueble>> GetInmueblesByPropConContratosAsync(int id);

    Task<Inmueble> GetInmuebleByIdAsync(int id);

    Task<Inmueble> CreateInmuebleAsync(Inmueble inmueble, IFormFile? image);

    Task UpdateInmuebleAsync(Inmueble inmueble, IFormFile? image);

    Task DeleteInmuebleAsync(int id);

    bool InmuebleExists(int id);

    Task<Inmueble> ApplyChanges(Inmueble existingInmueble, Inmueble inmueble);

    Task<byte[]?> GetArchivoByInmuebleIdAsync(int id);
}
