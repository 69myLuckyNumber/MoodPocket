using System;
using System.Security.Cryptography;
using System.Text;


namespace MoodPocket.WebUI.Utilities
{
	public static class PasswordHelperUtility
	{
		public static string HashPassword(string password, string salt)
		{
			var combinedPassword = String.Concat(password, salt);
			var sha256 = new SHA256Managed();
			var bytes = Encoding.UTF8.GetBytes(combinedPassword);
			var hash = sha256.ComputeHash(bytes);
			return Convert.ToBase64String(hash);
		}
		public static string GetRandomSalt(Int32 size = 12)
		{
			var random = new RNGCryptoServiceProvider();
			var salt = new Byte[size];
			random.GetBytes(salt);
			return Convert.ToBase64String(salt);
		}
		public static bool ValidatePassword(string enteredPassword, string storedHash, string storedSalt)
		{
			// Consider this function as an internal function where parameters like
			// storedHash and storedSalt are read from the database and then passed.

			var hash = HashPassword(enteredPassword, storedSalt);
			return string.Equals(storedHash, hash);
		}
	}
}