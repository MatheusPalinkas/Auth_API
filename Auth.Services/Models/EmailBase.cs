using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Models
{
    public abstract class EmailBase
    {
        protected readonly IConfiguration _configuration;

        public string Subject { get; set; }
        public string MailTo { get; set; }

        public EmailBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract string GenerateBody();

        public EmailBase AddSubject(string subject)
        {
            Subject = subject;
            return this;
        }
        public EmailBase AddMailTo(string mailTo)
        {
            MailTo = mailTo;
            return this;
        }

        public MailMessage Build()
        {
            EnsureArguments();

            var emailMessage = new MailMessage()
            {
                From = new MailAddress(_configuration["Email:UserName"], Subject),
                Body = GenerateBody(),
                Subject = Subject,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
            };

            emailMessage.To.Add(MailTo);

            return emailMessage;
        }

        protected virtual void EnsureArguments()
        {
            if (string.IsNullOrWhiteSpace(this.Subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrWhiteSpace(this.MailTo))
                throw new ArgumentNullException("MailTo");
        }
    }
}
