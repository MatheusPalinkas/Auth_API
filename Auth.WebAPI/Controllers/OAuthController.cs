using Auth.Application.Interfaces;
using Auth.Application.Validators;
using Auth.Application.ViewModels;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ITokenAppService _tokenAppService;

        public OAuthController(
            IUserAppService userAppService,
            ITokenAppService tokenAppService)
        {
            _userAppService = userAppService;
            _tokenAppService = tokenAppService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarViewModel request)
        {
            var result = await _userAppService.Registrar(request);

            if (!result.IsSucessed)
                return BadRequest(result);

            var tokenResponse = await _tokenAppService.RetornarJwt(request.Email);
            return Ok(tokenResponse);
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] LoginViewModel request)
        {
            var result = await _userAppService.Autenticar(request);

            if (!result.IsSucessed)
                return Unauthorized(new Response() { Errors = new List<string> { "Login ou senha incorretos" } });

            var tokenResponse = await _tokenAppService.RetornarJwt(request.Email);
            return Ok(tokenResponse);
        }

        [HttpPost("recuperar-senha")]
        public async Task<IActionResult> RecuperarSenha([FromBody] RecuperarSenhaViewModel request)
        {
            var result = await _userAppService.RecuperarSenha(request);

            if (!result.IsSucessed)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaViewModel request)
        {
            var result = await _userAppService.AlterarSenha(request);

            if (!result.IsSucessed)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
