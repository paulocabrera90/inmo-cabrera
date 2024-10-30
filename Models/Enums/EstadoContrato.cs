using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models;

public enum EstadoContrato
{

    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Vigente")]
    Vigente,

    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Cancelado")]
    Cancelado,    

    [EnumDataType(typeof(EstadoContrato))]
    [Display(Name = "Finalizado")]
    Finalizado
}