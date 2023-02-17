using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(MailMessage email);
    }
}
