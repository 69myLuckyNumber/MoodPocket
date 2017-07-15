
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{

	public class User
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Username is required")]
		[StringLength(24, MinimumLength = 4, ErrorMessage = "Usename must be from 4 to 32 characters long")]
		public string Username { get; set; }

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(32, MinimumLength = 4 ,ErrorMessage = "Password must be from 4 to 32 characters long")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage ="Confirm your password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

	}
}
