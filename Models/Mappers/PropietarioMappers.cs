using Inmueble_cabrera.Models;

namespace Inmueble_cabrera.Models.Mappers;

public class PropietarioMapper
{
    public static PropietarioResponse? ToMapper(Propietario propietario)
    {
        if (propietario == null) return null;

        return new PropietarioResponse
        {
            Id = propietario.Id,
            Dni = propietario.Dni,
            Nombre = propietario.Nombre,
            Apellido = propietario.Apellido,
            Email = propietario.Email,
            Direccion = propietario.Direccion,
            FechaCreacion = propietario.FechaCreacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            FechaActualizacion = propietario.FechaActualizacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            TelefonoArea = propietario.TelefonoArea,
            TelefonoNumero = propietario.TelefonoNumero
        };
    }
}