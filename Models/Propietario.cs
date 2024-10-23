namespace Inmueble_cabrera.Models;
using System.ComponentModel.DataAnnotations;

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
    public DateTime Fecha_Creacion { get; set; }
    public DateTime Fecha_Actualizacion { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "El area solo debe tener dígitos.")]	
    public string? Telefono_Area { get; set; }
    [RegularExpression(@"^\d+$", ErrorMessage = "El numero solo debe tener dígitos.")]	
    public string? Telefono_Numero { get; set; }
    public int Estado { get; set; } = 1;

    public string? Reset_Token { get; set;}
    public DateTime? Reset_Token_Expires { get; set;}
    public List<Inmueble> Inmuebles = new List<Inmueble>();

    public string NombreCompleto () => $"{Apellido} {Nombre}";

}
