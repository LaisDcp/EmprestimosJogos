using AutoMapper;
using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Domain.Interfaces.UoW;
using Microsoft.AspNetCore.Identity;
using System;

namespace EmprestimosJogos.Application.Services
{
    public class JogoAppService : IJogoAppService
    {
        private readonly IJogoRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPerfilRepository _repositoryPerfil;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IAuthFacade _facadeAuth;
        private readonly UserManager<Usuario> _userManager;

        public JogoAppService(IJogoRepository repository,
                                 IUsuarioRepository repositoryAutenticacao,
                                 IPerfilRepository repositoryPerfil,
                                 UserManager<Usuario> userManager,
                                 IAuthFacade facadeAuth,
                                 IMapper mapper,
                                 IUnitOfWork uow)
        {
            _repository = repository;
            _repositoryUsuario = repositoryAutenticacao;
            _repositoryPerfil = repositoryPerfil;
            _mapper = mapper;
            _uow = uow;
            _facadeAuth = facadeAuth;
            _userManager = userManager;
        }

        public ModelCountViewModel<JogoViewModel> GetByFilter(FilterContainsViewModel filter)
        {
            throw new NotImplementedException();
        }

        public JogoViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Create(JogoViewModel jogo)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(JogoViewModel jogo, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}