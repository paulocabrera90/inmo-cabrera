namespace Inmueble_cabrera.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Inmueble
{
    
    [Required(ErrorMessage = "El id es obligatorio.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "La direccion es obligatoria.")]
    public string Direccion { get; set; } = "";
    
    [Required(ErrorMessage = "El tipo de uso es obligatorio.")]
    public int Id_Tipo_Inmueble_Uso { get; set; }
    
    [ForeignKey("Id_Tipo_Inmueble_Uso")]
    public Tipo_Inmueble_Uso? Tipo_Uso { get; set; }
    
    [Required(ErrorMessage = "El tipo es obligatorio.")] 
    public int Id_Tipo_Inmueble { get; set; }

    [ForeignKey("Id_Tipo_Inmueble")]
    public Tipo_Inmueble? Tipo { get; set; }
    [Required(ErrorMessage = "Los ambientes son obligatorios.")]
    public int Ambientes { get; set; }
    [Required(ErrorMessage = "La latitud es obligatoria.")]
    public string Coordenada_Lat { get; set; } = "-33.30158732843527";
    [Required(ErrorMessage = "La longitud es obligatoria.")]
    public string Coordenada_Lon { get; set; } = "-66.33797013891889";
    [Required(ErrorMessage = "El precio es obligatorio.")]
    public decimal Precio { get; set; }
    [Required(ErrorMessage = "El id del propietario es obligatorio.")]
    public int Id_Propietario { get; set; }

    public bool Activo { get; set; }
    
    [NotMapped]
    public Propietario? Propietario { get; set; }
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Actualizacion { get; set; }
    
    [NotMapped]
    public string NombreInmueble => $"{Direccion} - {Tipo?.Descripcion}";
}
