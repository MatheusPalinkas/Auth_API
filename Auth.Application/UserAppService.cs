using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Interfaces;
using Auth.Application.Validators;
using Auth.Application.ViewModels;
using Auth.Domain.Entities;
using Auth.Services.Models;
using Auth.Services.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Auth.Application
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserAppService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Response> Autenticar(LoginViewModel loginViewModel)
        {
            var validatorResult = new LoginValidator().Validate(loginViewModel);

            if (!validatorResult.IsValid)
            {
                return new Response
                {
                    IsSucessed = validatorResult.IsValid,
                    Errors = validatorResult.Errors.Select(error => error.ErrorMessage)
                };
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Senha, false, lockoutOnFailure: false);

            return new Response
            {
                IsSucessed = result.Succeeded
            };
        }

        public async Task<Response> RecuperarSenha(RecuperarSenhaViewModel recuperarSenhaViewModel)
        {
            var validatorResult = new RecuperarSenhaValidator().Validate(recuperarSenhaViewModel);

            if (!validatorResult.IsValid)
            {
                return new Response
                {
                    IsSucessed = validatorResult.IsValid,
                    Errors = validatorResult.Errors.Select(error => error.ErrorMessage)
                };
            }

            var user = await _userManager.FindByEmailAsync(recuperarSenhaViewModel.Email);

            if (user == null)
            {
                return new Response
                {
                    IsSucessed = false,
                    Errors = new List<string> { "Usuario não encontrato" }
                };
            }


            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var email = new EmailRecuperarSenha(_configuration)
                                .AddVariaveisHTML(user.Name, passwordResetToken)
                                .AddSubject("Recuperar senha")
                                .AddMailTo(user.Email)
                                .Build();

            new EmailService(_configuration).Send(email);


            return new Response
            {
                IsSucessed = true,
            };
        }

        public async Task<Response> AlterarSenha(AlterarSenhaViewModel alterarSenhaViewModel)
        {
            var validatorResult = new AlterarSenhaValidator().Validate(alterarSenhaViewModel);

            if (!validatorResult.IsValid)
            {
                return new Response
                {
                    IsSucessed = validatorResult.IsValid,
                    Errors = validatorResult.Errors.Select(error => error.ErrorMessage)
                };
            }

            var user = await _userManager.FindByEmailAsync(alterarSenhaViewModel.Email);

            if (user == null)
            {
                return new Response
                {
                    IsSucessed = false,
                    Errors = new List<string> { "Usuario não encontrato" }
                };
            }


            var result = await _userManager.ResetPasswordAsync(user, alterarSenhaViewModel.ResetToken, alterarSenhaViewModel.Senha);

            return new Response
            {
                IsSucessed = result.Succeeded,
                Errors
                = result?.Errors?.Select(error => error.Description)
            };
        }

        public async Task<Response> Registrar(RegistrarViewModel registerViewModel)
        {
            var validatorResult = new RegistrarValidator().Validate(registerViewModel);

            if (!validatorResult.IsValid)
            {
                return new Response
                {
                    IsSucessed = validatorResult.IsValid,
                    Errors = validatorResult.Errors.Select(error => error.ErrorMessage)
                };
            }

            var user = new User
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                Name = registerViewModel.Nome,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Senha);

            var email = new EmailCriacaoConta(_configuration)
                    .AddVariaveisHTML(user.Name)
                    .AddSubject("Confirmação criação de conta")
                    .AddMailTo(user.Email)
                    .Build();

            new EmailService(_configuration).Send(email);

            return new Response
            {
                IsSucessed = result.Succeeded,
                Errors = result.Errors.Select(error => error.Description),
            };
        }
    }
}
