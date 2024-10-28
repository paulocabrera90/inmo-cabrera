namespace Inmueble_cabrera.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Propietario
{
    public int Id { get; set; }
    [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El documento debe tener 7 u 8 dígitos.")]	
    public string Dni { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; } = "";
    public string? Direccion { get; set; }
    public string Usuario { get; set; } = "";
    public string Password { get; set; } = "";

    [Column("Fecha_Creacion")]
    public DateTime FechaCreacion { get; set; }

    [Column("Fecha_Actualizacion")]
    public DateTime FechaActualizacion { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "El area solo debe tener dígitos.")]
    [Column("Telefono_Area")]
    public string? TelefonoArea { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "El numero solo debe tener dígitos.")]
    [Column("Telefono_Numero")]
    public string? TelefonoNumero { get; set; }
    public int Estado { get; set; } = 1;
    
    [Column("Reset_Token")]
    public string? ResetToken { get; set;}

    [Column("Reset_Token_Expires")]
    public DateTime? ResetTokenExpires { get; set;}
    public List<Inmueble> Inmuebles = new List<Inmueble>();

    public string NombreCompleto () => $"{Apellido} {Nombre}";

}
