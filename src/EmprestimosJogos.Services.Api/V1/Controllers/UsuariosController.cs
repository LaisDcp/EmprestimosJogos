using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmprestimosJogos.Services.Api.V1.Controllers
{
    [Authorize, Route("[controller]"), ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _service;

        public UsuariosController(IUsuarioAppService service) : base()
        {
            _service = service;
        }

        [HttpPost("autenticar"), AllowAnonymous]
        [ProducesResponseType(typeof(RetornoAutenticacaoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate(UsuarioViewModel autenticacao)
        {
            RetornoAutenticacaoViewModel _retorno = await _service.Authenticate(autenticacao);

            return Ok(_retorno);
        }

        [HttpPut("alterar-senha")]
        [ProducesResponseType(typeof(RetornoAutenticacaoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha)
        {
            await _service.AlterarSenha(alterarSenha);

            return Ok();
        }

        [HttpPost("reset-senha"), AllowAnonymous]
        [ProducesResponseType(typeof(RetornoAutenticacaoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetSenha(ResetSenhaUsuarioViewModel resetSenha)
        {
            await _service.ResetSenha(resetSenha);
            return Ok();
        }

        [HttpGet("recuperar-minha-senha"), AllowAnonymous]
        public async Task<IActionResult> EsqueciSenha(string email)
        {
            await _service.RecuperarMinhaSenha(email);
            return Ok();
        }    }
}