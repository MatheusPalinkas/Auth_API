using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Services.Models
{
    public class EmailCriacaoConta : EmailBase
    {
        public string Nome { get; set; }

        public EmailCriacaoConta(IConfiguration configuration)
            : base(configuration)
        {
        }

        public EmailCriacaoConta AddVariaveisHTML(string nome)
        {
            Nome = nome;

            return this;
        }

        protected override void EnsureArguments()
        {
            if (string.IsNullOrWhiteSpace(this.Nome))
                throw new ArgumentNullException("Nome");

            base.EnsureArguments();
        }

        public override string GenerateBody()
        {
            var content = GetTemplate();

            content = content.Replace("#NOME#", Nome);
            content = content.Replace("#URL_LOGIN#", _configuration["Front:BaseURL"]);

            return content;
        }

        private string GetTemplate()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Auth.Services\\Templates\\CriacaoConta.html");
            return System.IO.File.ReadAllText(path);
        }
    }
}
