using Inmueble_cabrera.Models;
using Inmueble_cabrera.Models.ContratoModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inmueble_cabrera.Repository;
public interface IContratosRepository
{
    Task<IEnumerable<Contrato>> GetAllContratosAsync();
    
    Task<Contrato> GetContratoByIdAsync(int id);

    Task<Contrato> CreateContratoAsync(Contrato contrato);

    Task UpdateContratoAsync(Contrato contrato);

    Task DeleteContratoAsync(int id);

    bool ContratoExists(int id);
}
