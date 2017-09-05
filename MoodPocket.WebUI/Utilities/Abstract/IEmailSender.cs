using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodPocket.WebUI.Utilities.Abstract
{
    public interface IEmailSender
    {
        string CoreEmail { get; }

        void SendVerificationLink(string name, string to);
    }
}
