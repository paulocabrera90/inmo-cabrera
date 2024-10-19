using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models.Authentication;

public class ChangePasswordView {
    public required int Id { get; set; }
    
    [DataType(DataType.EmailAddress)]
	public required string Email { get; set; }
    [DataType(DataType.Password)]
    public required string CurrentPassword { get; set; }
    
    [DataType(DataType.Password)]
    public required string NewPassword { get; set; }
}
