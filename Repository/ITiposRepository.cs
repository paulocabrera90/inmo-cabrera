using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Repository;
public interface ITiposRepository
{
    Task<IEnumerable<TipoInmueble>> GetTiposInmueblesAsync(); 
    Task<IEnumerable<TipoInmuebleUso>> GetTiposInmueblesUsoAsync(); 
}
