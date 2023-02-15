using Auth.Application.ViewModels;
using Auth.Domain.Entities;

namespace Auth.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<Response> Autenticar(LoginViewModel loginViewModel);
        Task<Response> Registrar(RegistrarViewModel registerViewModel);
        Task<Response> RecuperarSenha(RecuperarSenhaViewModel recuperarSenhaViewModel);
        Task<Response> AlterarSenha(AlterarSenhaViewModel alterarSenhaViewModel);
    }
}