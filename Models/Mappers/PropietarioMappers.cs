using InmobiliariaCA.Models;

namespace Inmueble_cabrera.Models.Mappers;

public class PropietarioMapper
{
    public static PropietarioResponse ToMapper(Propietario propietario)
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
            FechaCreacion = propietario.Fecha_Creacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            FechaActualizacion = propietario.Fecha_Actualizacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            TelefonoArea = propietario.Telefono_Area,
            TelefonoNumero = propietario.Telefono_Numero
        };
    }
}