using InmobiliariaCA.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Inmueble_cabrera.Models;
public interface IPropietariosRepository
{
    Task<IEnumerable<Propietario>> GetAllPropietariosAsync();
    Task<Propietario> GetPropietarioByIdAsync(int id);
    Task<Propietario> CreatePropietarioAsync(Propietario propietario);
    Task UpdatePropietarioAsync(Propietario propietario);
    Task DeletePropietarioAsync(int id);
    bool PropietarioExists(int id);
    Task<Propietario> ApplyChanges(Propietario existingPropietario, Propietario propietario);
}
