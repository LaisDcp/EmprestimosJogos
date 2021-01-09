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
using System;

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
            throw new NotImplementedException();
        }

        public bool Create(AmigoViewModel amigo)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            Amigo _amigo = _repository.GetById(id);
            if (_amigo == null)
                throw new ApiException(ApiErrorCodes.INVAMIGO);

            _repository.Delete(_amigo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Edit(AmigoViewModel amigo, Guid id, Guid usuarioId)
        {
            if (!_repositoryUsuario.ExistsWithId(usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            ValidationResult _result = new AmigoValidation().Validate(amigo);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Amigo _amigo = _repository.GetById(id);

            if (_amigo == null)
                throw new ApiException(ApiErrorCodes.INVAMIGO);

            _amigo = _mapper.Map(amigo, _amigo);

            _amigo.SetCreatorId(usuarioId);

            _repository.Update(_amigo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }
    }
}