using MoodPocket.WebUI.Utilities.Abstract;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoodPocket.WebUI.Utilities.Concrete
{
    public class StringHashService : IStringHasher
    {
        public string HashString(string str, string salt)
        {
            var combinedPassword = String.Concat(str, salt);
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = Encoding.UTF8.GetString(sha256.ComputeHash(bytes));

            return Regex.Replace(hash, "[^A-Za-z0-9_]", String.Empty);
        }
        public string GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Regex.Replace(Convert.ToBase64String(salt), "[^A-Za-z0-9_]", String.Empty);
        }
        public bool ValidateHashedString(string enteredString, string storedHash, string storedSalt)
        {
            var hash = HashString(enteredString, storedSalt);
            return string.Equals(storedHash, hash);
        }
    }
}