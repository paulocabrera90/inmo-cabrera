using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Inmueble_cabrera.Models.ContratoModels;
public class Contrato
{

    public Contrato()
    {

    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El inmueble es obligatorio.")]
    [ForeignKey("Inmueble")]
    [Column("Id_Inmueble")]
    public int IdInmueble { get; set; }

    [Required(ErrorMessage = "El inquilino es obligatorio.")]
    [ForeignKey("Inquilino")]
    [Column("Id_Inquilino")]
    public int IdInquilino { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    [DataType(DataType.Date)]
    [Column("Fecha_Desde")]
    public DateTime FechaDesde { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
    [DataType(DataType.Date)]
    [Column("Fecha_Hasta")]
    public DateTime FechaHasta { get; set; } = DateTime.Today.AddDays(30);

    [Required(ErrorMessage = "El monto del alquiler es obligatorio.")]
    [Column("Monto_Alquiler", TypeName = "decimal(10, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo.")]
    public decimal MontoAlquiler { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Column("Fecha_Finalizacion_Anticipada")]
    public DateTime? FechaFinalizacionAnticipada { get; set; }

    [Column("multa", TypeName = "decimal(10, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "La multa debe ser un valor positivo.")]
    public decimal? Multa { get; set; }   

    [DataType(DataType.DateTime)]
    [Column("Fecha_Creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [DataType(DataType.DateTime)]
    [Column("Fecha_Actualizacion")]
    public DateTime FechaActualizacion { get; set; } = DateTime.Now;

    [Column("Cantidad_Cuotas")]
    public int CantidadCuotas { get; set; }

    [Column("Cuotas_Pagas")]
    public int CuotasPagas { get; set; }
    public bool Pagado { get; set; } = false;

    [ForeignKey("IdInmueble")]
    public virtual Inmueble? Inmueble { get; set; }

    [ForeignKey("IdInquilino")]
    public virtual Inquilino? Inquilino { get; set; }
    
     [Column("Estado", TypeName = "varchar(15)")]
    public EstadoContrato Estado { get; set; } = EstadoContrato.Vigente;

    public string MontoAlquilerString() => MontoAlquiler.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));

    public string? MultaString() => Multa?.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));

    public void MultaCalculada()
    {
        if (FechaFinalizacionAnticipada.HasValue)
        {
            TimeSpan tiempoTranscurrido = FechaFinalizacionAnticipada.Value.Subtract(FechaDesde);
            TimeSpan tiempoTotal = FechaHasta.Subtract(FechaDesde);

            if (tiempoTranscurrido.TotalDays < tiempoTotal.TotalDays / 2)
            {
                Multa = MontoAlquiler * 2;
            }
            else
            {
                Multa = MontoAlquiler;
            }
        }
    }

    public bool PagosCompletos() => CantidadCuotas == CuotasPagas;

    public bool EsFinalizado() => this.PagosCompletos() || EstadoContrato.Finalizado == Estado;

    public int CantidadCuotasTotal()
    {
        int meses = ((FechaHasta.Year - FechaDesde.Year) * 12) + FechaHasta.Month - FechaDesde.Month;
        return Math.Max(meses, 1);
    }

}
