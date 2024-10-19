using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models;

public enum EstadoContrato
{
    [Display(Name = "Cancelado")]
    Cancelado,

    [Display(Name = "Vigente")]
    Vigente,

    [Display(Name = "Finalizado")]
    Finalizado
}