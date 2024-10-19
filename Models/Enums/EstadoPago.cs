using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models;

public enum EstadoPago
{
    [Display(Name = "Pagado")]
    Pagado,

    [Display(Name = "Anulado")]
    Anulado,

    [Display(Name = "Pendiente")]
    Pendiente,

}