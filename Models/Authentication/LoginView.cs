using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models.Authentication;
public class LoginView
	{
		[DataType(DataType.EmailAddress)]
		public required string Email { get; set; }
        
		[DataType(DataType.Password)]
		public required string Password { get; set; }
	}