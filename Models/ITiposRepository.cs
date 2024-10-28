using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Models;
public interface ITiposRepository
{
    Task<IEnumerable<TipoInmueble>> GetTiposInmueblesAsync(); 
    Task<IEnumerable<TipoInmuebleUso>> GetTiposInmueblesUsoAsync(); 
}
