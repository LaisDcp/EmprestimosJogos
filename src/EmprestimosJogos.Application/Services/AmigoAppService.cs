using AutoMapper;
using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.Validations;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Domain.Interfaces.UoW;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EmprestimosJogos.Application.Services
{
    public class AmigoAppService : IAmigoAppService
    {
        private readonly IAmigoRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public AmigoAppService(IAmigoRepository repository,
                                 IUsuarioRepository repositoryAutenticacao,
                                 IMapper mapper,
                                 IUnitOfWork uow)
        {
            _repository = repository;
            _repositoryUsuario = repositoryAutenticacao;
            _mapper = mapper;
            _uow = uow;
        }

        public ModelCountViewModel<AmigoViewModel> GetByFilter(FilterContainsViewModel filter)
        {
            throw new NotImplementedException();
        }

        public AmigoViewModel GetById(Guid id)
        {
            Amigo _amigo = _repository.GetById(id);

            if (_amigo == null)
                throw new ApiException(ApiErrorCodes.INVAMIGO);

            return _mapper.Map<AmigoViewModel>(_amigo);
        }

        public bool Create(AmigoViewModel amigo, Guid usuarioId)
        {
            if(_repositoryUsuario.ExistsWithId(usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            ValidationResult _result = new AmigoValidation().Validate(amigo);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Amigo _amigo = _mapper.Map<Amigo>(amigo);

            _amigo.SetCreatorId(usuarioId);

            _repository.Create(_amigo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Delete(Guid id)
        {
            Amigo _amigo = _repository.GetById(id, q => q.Include(i => i.JogosEmprestados));

            if (_amigo == null)
                throw new ApiException(ApiErrorCodes.INVAMIGO);

            if (_amigo.JogosEmprestados.Any())
                throw new ApiException(ApiErrorCodes.AMIHASJOGO);

            _repository.Delete(_amigo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Edit(AmigoViewModel amigo, Guid id)
        {
            ValidationResult _result = new AmigoValidation().Validate(amigo);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Amigo _amigo = _repository.GetById(id);

            if (_amigo == null)
                throw new ApiException(ApiErrorCodes.INVAMIGO);

            _amigo = _mapper.Map(amigo, _amigo);

            _repository.Update(_amigo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }
    }
}