namespace Inmueble_cabrera.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
    
    public int Id { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria.")]
    public string Direccion { get; set; } = "";
    
    [Required(ErrorMessage = "El tipo de uso es obligatorio.")]
    [Column("Id_Tipo_Inmueble_Uso")]
    public int IdTipoInmuebleUso { get; set; }
    
    [ForeignKey("IdTipoInmuebleUso")]
    public TipoInmuebleUso? TipoUso { get; set; }
    
    [Required(ErrorMessage = "El tipo es obligatorio.")]
    [Column("Id_Tipo_Inmueble")]
    public int IdTipoInmueble { get; set; }

    [ForeignKey("IdTipoInmueble")]
    public TipoInmueble? Tipo { get; set; }

    [Required(ErrorMessage = "Los ambientes son obligatorios.")]
    public int Ambientes { get; set; }

    [Column("Coordenada_Lat")]
    [Required(ErrorMessage = "La latitud es obligatoria.")]
    public string CoordenadaLat { get; set; } = "-33.30158732843527";
    
    [Column("Coordenada_Lon")]
    [Required(ErrorMessage = "La longitud es obligatoria.")]
    public string CoordenadaLon { get; set; } = "-66.33797013891889";
    
    [Required(ErrorMessage = "El precio es obligatorio.")]
    public decimal Precio { get; set; }

    [Column("Id_Propietario")]
    [Required(ErrorMessage = "El id del propietario es obligatorio.")]
    public int IdPropietario { get; set; }

    public bool Activo { get; set; }    
    
    [Column("Fecha_Creacion")]
    public DateTime FechaCreacion { get; set; }
    
    [Column("Fecha_Actualizacion")]
    public DateTime FechaActualizacion { get; set; }

    public byte[]? ImageBlob { get; set; }
    
    [NotMapped]
    public IFormFile? Archivo { get; set; }
    
    [NotMapped]
    public string NombreInmueble => $"{Direccion} - {Tipo?.Descripcion}";

    [NotMapped]
    public Propietario? Propietario { get; set; }
}
