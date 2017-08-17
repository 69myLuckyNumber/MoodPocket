
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPocket.Domain.Entities
{

	public class User
	{
		[Key]
		public int Id { get; set; }

		public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public string Salt { get; set; }

		public bool IsVerified { get; set; }

		public virtual Gallery Gallery { get; set; }
	}
}
