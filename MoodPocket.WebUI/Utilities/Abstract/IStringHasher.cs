using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.WebUI.Utilities.Abstract
{
    public interface IStringHasher
    {
        string HashString(string str, string salt);
        string GetRandomSalt(Int32 size = 12);
        bool ValidateHashedString(string enteredString, string storedHash, string storedSalt);

    }
}
