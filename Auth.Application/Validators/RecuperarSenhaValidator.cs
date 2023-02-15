using Auth.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validators
{
    public class RecuperarSenhaValidator : AbstractValidator<RecuperarSenhaViewModel>
    {
        public RecuperarSenhaValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
