using Inmueble_cabrera.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Inmueble_cabrera.Repository;
public interface IPropietariosRepository
{
    Task<IEnumerable<Propietario>> GetAllPropietariosAsync();
    Task<Propietario> GetPropietarioByIdAsync(int id);
    Task<Propietario> CreatePropietarioAsync(Propietario propietario);
    Task UpdatePropietarioAsync(Propietario propietario);
    Task DeletePropietarioAsync(int id);
    bool PropietarioExists(int id);
    Task<Propietario> ApplyChanges(Propietario existingPropietario, Propietario propietario);
    Task<bool> ResetPassword(string email);
    Task<bool> VerifyNumberStatusAsync(string email, string verificationNumber);
}
