using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.ViewModels
{
    public class AlterarSenhaViewModel
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
