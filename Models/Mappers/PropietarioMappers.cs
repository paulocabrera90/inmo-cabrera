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
            Fecha_creacion = propietario.Fecha_Creacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            Fecha_actualizacion = propietario.Fecha_Actualizacion.ToString("yyyy-MM-ddTHH:mm:ss"),
            Telefono_area = propietario.Telefono_Area,
            Telefono_numero = propietario.Telefono_Numero
        };
    }
}