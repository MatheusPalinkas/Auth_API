﻿using Auth.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Validators
{
    public class AlterarSenhaValidator : AbstractValidator<AlterarSenhaViewModel>
    {
        public AlterarSenhaValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(e => e.ResetToken)
                .NotEmpty().WithMessage("Reset token é obrigatório");

            RuleFor(e => e.Senha)
                .NotEmpty().WithMessage("Senha é obrigatório");

            RuleFor(e => e.ConfirmarSenha)
                .NotEmpty().WithMessage("Confirmar Senha é obrigatório")
                .Equal(e => e.Senha).WithMessage("Senha e confirmar senha devem ser iguais");
        }
    }
}
