using Auth.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validators
{
    public class RegistrarValidator : AbstractValidator<RegistrarViewModel>
    {
        public RegistrarValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(e => e.Senha)
                .NotEmpty().WithMessage("Senha é obrigatório")
                .MinimumLength(8).WithMessage("Senha deve ter ao menos 8 caracteres");

            RuleFor(e => e.ConfirmarSenha)
                .NotEmpty().WithMessage("Confirmar Senha é obrigatório")
                .MinimumLength(8).WithMessage("Confirmar Senha deve ter ao menos 8 caracteres")
                .Equal(e => e.Senha).WithMessage("Senha e confirmar senha devem ser iguais");
        }
    }
}
