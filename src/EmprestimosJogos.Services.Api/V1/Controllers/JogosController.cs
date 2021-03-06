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

namespace EmprestimosJogos.Services.Api.V1.Controllers
{
    [HasPermission(nameof(Perfil.ADM))]
    [Authorize, Route("[controller]"), ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoAppService _service;

        public JogosController(IJogoAppService service) : base()
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Create(NomeBaseViewModel jogo)
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
              !Guid.TryParse(uId, out Guid usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            bool _result = _service.Create(jogo, usuarioId);

            return Ok(_result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JogoViewModel), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            JogoViewModel _result = _service.GetById(id);

            return Ok(_result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public IActionResult Delete(Guid id)
        {
            bool _result = _service.Delete(id);
            return Ok(_result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Edit(NomeBaseViewModel jogo, Guid id)
        {
            bool _result = _service.Edit(jogo, id);

            return Ok(_result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ModelCountViewModel<JogoViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetByFilter([FromBody] FilterPaginacaoViewModel filter)
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                 !Guid.TryParse(uId, out Guid usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            ModelCountViewModel<JogoViewModel> _result = _service.GetByFilter(filter, usuarioId);
            return Ok(_result);
        }        [HttpPut("{id}/emprestar")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Emprestar(Guid id, [FromQuery] Guid amigoId)
        {
            bool _result = _service.Emprestar(id, amigoId);

            return Ok(_result);
        }        [HttpPut("{id}/devolver")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Devolver(Guid id)
        {
            bool _result = _service.Devolver(id);

            return Ok(_result);
        }    }
}