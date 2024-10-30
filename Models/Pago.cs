namespace Inmueble_cabrera.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inmueble_cabrera.Models.ContratoModels;

public class Pago {
    public Pago()
    {
        
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "El ID del contrato es obligatorio.")]
    [Column("Contrato_Id")]
    public int ContratoId { get; set; }

    [Required(ErrorMessage = "El número de pago es obligatorio.")]
    [Column("Numero_Pago")]
    public int NumeroPago { get; set; }

    [Required(ErrorMessage = "La fecha de pago es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de fecha no válido.")]
    [Column("Fecha_Pago")]
    public DateTime FechaPago { get; set; }

    [StringLength(255, ErrorMessage = "El detalle no puede exceder los 255 caracteres.")]
    public string Detalle { get; set; } = "";

    [Required(ErrorMessage = "El importe es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El importe debe ser mayor que cero.")]
    public decimal Importe { get; set; }

    [Column("Estado", TypeName = "varchar(15)")]
    public EstadoPago Estado { get; set; } = EstadoPago.Pagado;

    [Column("Fecha_Anulacion")]
    public DateTime? FechaAnulacion { get; set; }

    [DataType(DataType.DateTime)]
    [Column("Fecha_Creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [DataType(DataType.DateTime)]
    [Column("Fecha_Actualizacion")]
    public DateTime FechaActualizacion { get; set; } = DateTime.Now;


    [ForeignKey("ContratoId")]
    public virtual Contrato? Contrato { get; set; }

}
