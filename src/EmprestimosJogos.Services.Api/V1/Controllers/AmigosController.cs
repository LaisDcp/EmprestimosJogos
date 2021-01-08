using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(AmigoViewModel usuario)
        {
            bool _result = _service.Create(usuario);

            return Ok(_result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AmigoViewModel), StatusCodes.Status200OK)]
        public IActionResult GetByEmail(Guid id)
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
        public IActionResult Edit(AmigoViewModel usuario, Guid id)
        {
            bool _result = _service.Edit(usuario, id);

            return Ok(_result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ModelCountViewModel<AmigoViewModel>), StatusCodes.Status200OK)]
        public IActionResult GetByFilter([FromBody] FilterUsuarioViewModel filter)
        {
            ModelCountViewModel<AmigoViewModel> _result = _service.GetByFilter(filter);
            return Ok(_result);
        }    }
}