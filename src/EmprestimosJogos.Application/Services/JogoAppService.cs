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
    public class JogoAppService : IJogoAppService
    {
        private readonly IJogoRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public JogoAppService(IJogoRepository repository,
                                 IUsuarioRepository repositoryAutenticacao,
                                 IMapper mapper,
                                 IUnitOfWork uow)
        {
            _repository = repository;
            _repositoryUsuario = repositoryAutenticacao;
            _mapper = mapper;
            _uow = uow;
        }

        public ModelCountViewModel<JogoViewModel> GetByFilter(FilterContainsViewModel filter)
        {
            throw new NotImplementedException();
        }

        public JogoViewModel GetById(Guid id)
        {
            Jogo _jogo = _repository.GetById(id);

            if (_jogo == null)
                throw new ApiException(ApiErrorCodes.INVJOGO);

            return _mapper.Map<JogoViewModel>(_jogo);
        }

        public bool Create(JogoViewModel jogo, Guid usuarioId)
        {
            if (_repositoryUsuario.ExistsWithId(usuarioId))
                throw new ApiException(ApiErrorCodes.INVUSU);

            ValidationResult _result = new JogoValidation().Validate(jogo);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Jogo _jogo = _mapper.Map<Jogo>(jogo);

            _jogo.SetCreatorId(usuarioId);

            _repository.Create(_jogo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Delete(Guid id)
        {
            Jogo _jogo = _repository.GetById(id);
            if (_jogo == null)
                throw new ApiException(ApiErrorCodes.INVJOGO);

            _repository.Delete(_jogo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Edit(JogoViewModel jogo, Guid id)
        {
            ValidationResult _result = new JogoValidation().Validate(jogo);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Jogo _jogo = _repository.GetById(id);

            if (_jogo == null)
                throw new ApiException(ApiErrorCodes.INVJOGO);

            _jogo = _mapper.Map(jogo, _jogo);

            _repository.Update(_jogo);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }
    }
}