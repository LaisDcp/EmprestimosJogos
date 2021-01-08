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
    public class AmigoAppService : IAmigoAppService
    {
        private readonly IAmigoRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;
        private readonly IPerfilRepository _repositoryPerfil;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IAuthFacade _facadeAuth;
        private readonly UserManager<Usuario> _userManager;

        public AmigoAppService(IAmigoRepository repository,
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

        public ModelCountViewModel<AmigoViewModel> GetByFilter(FilterUsuarioViewModel filter)
        {
            throw new NotImplementedException();
        }

        public AmigoViewModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Create(AmigoViewModel usuario)
        {
            throw new NotImplementedException();

        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(AmigoViewModel usuario, Guid id)
        {
            throw new NotImplementedException();

        }
    }
}