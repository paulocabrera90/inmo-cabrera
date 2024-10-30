using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models;

public enum EstadoPago
{
    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Pagado")]
    Pagado,

    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Anulado")]
    Anulado,

    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Pendiente")]
    Pendiente,

}