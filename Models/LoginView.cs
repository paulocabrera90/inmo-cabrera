using System.ComponentModel.DataAnnotations;

namespace InmobiliariaCA.Models;
public class LoginView
	{
		[DataType(DataType.EmailAddress)]
		public required string Usuario { get; set; }
        
		[DataType(DataType.Password)]
		public required string Password { get; set; }
	}