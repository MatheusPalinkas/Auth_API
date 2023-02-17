using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Models
{
    public class EmailRecuperarSenha : EmailBase
    {
        public string Nome { get; set; }
        public string ResetToken { get; set; }

        public EmailRecuperarSenha(IConfiguration configuration)
            : base(configuration)
        {
        }

        public EmailRecuperarSenha AddVariaveisHTML(string nome, string resetToken)
        {
            Nome = nome;
            ResetToken = resetToken;

            return this;
        }

        protected override void EnsureArguments()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentNullException("Nome");

            if (string.IsNullOrWhiteSpace(this.ResetToken))
                throw new ArgumentNullException("ResetToken");

            base.EnsureArguments();
        }

        public override string GenerateBody()
        {
            var content = GetTemplate();

            content = content.Replace("#NOME#", Nome);
            content = content.Replace("#URL#", UrlWithResetToken());

            return content;
        }
        private string UrlWithResetToken()
        {
            var baseUrl = _configuration["Front:AlterarSenhaURL"];

            return $"{baseUrl}?email={this.MailTo}&resetToken={this.ResetToken}";
        }

        private string GetTemplate()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Auth.Services\\Templates\\RecuperarSenha.html");
            return System.IO.File.ReadAllText(path);
        }
    }
}
