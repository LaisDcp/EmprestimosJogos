using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Infra.CrossCutting.Auth.Providers;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using EmprestimosJogos.Services.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
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

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateUsuarioViewModel usuario)
        {
            bool _result = await _service.CreateAsync(usuario);

            return Ok(_result);
        }

        [HttpGet]
        [HasPermission(nameof(Perfil.ADM))]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public IActionResult GetById()
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                 !Guid.TryParse(uId, out Guid id))
                throw new ApiException(ApiErrorCodes.INVUSU);

            UsuarioViewModel _result = _service.GetById(id);

            return Ok(_result);
        }

        [HttpDelete]
        [HasPermission(nameof(Perfil.ADM))]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public IActionResult Delete()
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                    !Guid.TryParse(uId, out Guid id))
                throw new ApiException(ApiErrorCodes.INVUSU);

            bool _result = _service.Delete(id);
            return Ok(_result);
        }

        [HttpPut]
        [HasPermission(nameof(Perfil.ADM))]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Edit(NomeBaseViewModel usuario)
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                 !Guid.TryParse(uId, out Guid id))
                throw new ApiException(ApiErrorCodes.INVUSU);

            bool _result = _service.Edit(usuario, id);

            return Ok(_result);
        }

        [HttpPost("autenticar"), AllowAnonymous]
        [ProducesResponseType(typeof(RetornoAutenticacaoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate(LoginViewModel autenticacao)
        {
            RetornoAutenticacaoViewModel _retorno = await _service.Authenticate(autenticacao);

            return Ok(_retorno);
        }

        [HttpPut("alterar-senha")]
        [HasPermission(nameof(Perfil.ADM))]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha)
        {
            await _service.AlterarSenha(alterarSenha);

            return Ok();
        }

        [HttpPost("reset-senha"), AllowAnonymous]
        public async Task<IActionResult> ResetSenha(ResetSenhaUsuarioViewModel resetSenha)
        {
            await _service.ResetSenha(resetSenha);
            return Ok();
        }

        [HttpGet("recuperar-minha-senha"), AllowAnonymous]
        [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RecuperarMinhaSenha(string email)
        {
            TokenViewModel _result = await _service.RecuperarMinhaSenha(email);
            return Ok(_result);
        }    }
}