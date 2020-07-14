using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mirapi.Core.Interfaces
{
    public interface IMailHelper
    {
        Task Send(string subject, string message, string toEmail);
        bool IsValidEmail(string email);
    }
}
