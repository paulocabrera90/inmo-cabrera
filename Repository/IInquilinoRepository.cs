using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Repository;
public interface IInquilinoRepository
{
    Task<IEnumerable<Inquilino>> GetInquilinosAsync(); 
    Task<Inquilino?> GetInquilinoByInmuebleAsync(int idInmueble);
}
