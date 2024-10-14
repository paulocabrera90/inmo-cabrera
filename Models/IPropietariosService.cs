using InmobiliariaCA.Models;

namespace Inmueble_cabrera.Models;
public interface IPropietariosService
{
    Task<IEnumerable<Propietario>> GetAllPropietariosAsync();
    Task<Propietario> GetPropietarioByIdAsync(int id);
    Task<Propietario> CreatePropietarioAsync(Propietario propietario);
    Task UpdatePropietarioAsync(Propietario propietario);
    Task DeletePropietarioAsync(int id);
    bool PropietarioExists(int id);
}
