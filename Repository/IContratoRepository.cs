using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inmueble_cabrera.Repository;
public interface IContratosRepository
{
    Task<IEnumerable<Contrato>> GetAllContratosAsync();
    Task<IEnumerable<Contrato>> GetContratoByIdVigentesAsync(int idPropietario, bool flagVigente);
    Task<Contrato> GetContratoByIdAsync(int id);
    bool ContratoExists(int id);
}
