using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using EmprestimosJogos.Services.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;

namespace EmprestimosJogos.Services.Api.V1.Controllers
{
    [Authorize, Route("[controller]"), ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly IAmigoAppService _service;

        public AmigosController(IAmigoAppService service) : base()
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult Create(AmigoViewModel amigo)
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                 !Guid.TryParse(uId, out Guid usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            bool _result = _service.Create(amigo, usuarioId);

            return Ok(_result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AmigoViewModel), StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            AmigoViewModel _result = _service.GetById(id);

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
        public IActionResult Edit(AmigoViewModel amigo, Guid id)
        {
            bool _result = _service.Edit(amigo, id);

            return Ok(_result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ModelCountViewModel<AmigoViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetByFilter([FromBody] FilterPaginacaoViewModel filter)
        {
            if (!Request.Headers.TryGetValue(ControllersConstants.UsuarioId, out StringValues uId) ||
                 !Guid.TryParse(uId, out Guid usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            ModelCountViewModel<AmigoViewModel> _result = _service.GetByFilter(filter, usuarioId);
            return Ok(_result);
        }    }
}