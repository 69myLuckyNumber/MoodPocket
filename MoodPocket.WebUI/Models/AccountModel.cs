using System.ComponentModel.DataAnnotations;
using MoodPocket.Domain.Entities;

namespace MoodPocket.WebUI.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage ="Username is required")]
		[Display(Name = "Username")]
		[StringLength(24, MinimumLength = 3, ErrorMessage = "Username must be from 3 to 24 characters long")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[Display(Name = "Email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(32, MinimumLength = 4, ErrorMessage = "Password must be from 4 to 32 characters long")]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Confirm your password")]
		[DataType(DataType.Password)]
		[Display(Name = "Password again")]
		public string ConfirmPassword { get; set; }

	}
}