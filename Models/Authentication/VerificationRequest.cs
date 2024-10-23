using System.ComponentModel.DataAnnotations;

namespace Inmueble_cabrera.Models.Authentication;
public class VerificationRequest
	{
		[DataType(DataType.EmailAddress)]
		public required string Email { get; set; }
        
		public required string VerificationNumber { get; set; }
	}