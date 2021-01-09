using AutoMapper;
using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.Validations;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Domain.Interfaces.UoW;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Infra.CrossCutting.Identity.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimosJogos.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenRepository _repositoryToken;
        private readonly ITokenTypeRepository _repositoryTokenType;
        private readonly IPerfilRepository _repositoryPerfil;

        private readonly ITokenFacade _facadeToken;
        private readonly IAuthFacade _facadeAuth;
        private readonly IPasswordValidator<Usuario> _validatorPassword;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioAppService(IUsuarioRepository repository,
                                 ITokenRepository repositoryToken,
                                 ITokenTypeRepository repositoryTokenType,
                                 IPerfilRepository repositoryPerfil,
                                 ITokenFacade facadeToken,
                                 IAuthFacade facadeAuth,
                                 IPasswordValidator<Usuario> validatorPassword,
                                 SignInManager<Usuario> signInManager,
                                 UserManager<Usuario> userManager,
                                 IMapper mapper,
                                 IUnitOfWork uow)
        {
            _repository = repository;
            _repositoryToken = repositoryToken;
            _repositoryTokenType = repositoryTokenType;
            _repositoryPerfil = repositoryPerfil;
            _facadeToken = facadeToken;
            _facadeAuth = facadeAuth;
            _validatorPassword = validatorPassword;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _uow = uow;
        }

        public UsuarioViewModel GetById(Guid id)
        {
            Usuario _usuario = _repository.GetById(id);

            if (_usuario == null)
                throw new ApiException(ApiErrorCodes.INVUSU);

            return _mapper.Map<UsuarioViewModel>(_usuario);
        }

        public async Task<bool> CreateAsync(CreateUsuarioViewModel usuario)
        {
            ValidationResult _result = new CreateUsuarioValidation().Validate(usuario);

            if (!_result.IsValid)
                throw new ApiException(_result.GetErrors(), ApiErrorCodes.MODNOTVALD);

            Perfil _perfil = _repositoryPerfil.GetByKey(Perfil.ADM);

            if (_repository.ExistsWithUserName(usuario.Email))
                throw new ApiException(ApiErrorCodes.EXUSU);

            Usuario _usuario = _mapper.Map<Usuario>(usuario);

            IdentityResult _identityResult = await _userManager.AddPasswordAsync(_usuario, usuario.Senha);

            if (!_identityResult.Succeeded)
                throw new ApiException(ApiErrorCodes.INVPASS, nameof(usuario.Senha));

            IdentityResult _createResult = await _userManager.CreateAsync(_usuario, usuario.Senha);

            if (!_createResult.Succeeded || _createResult.Errors.Any())
                throw new ApiException(_createResult.GetErrorsToString(), ApiErrorCodes.CRUSIDNT);

            _usuario.SetPerfilId(_perfil.Id);

            _repository.Create(_usuario);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Delete(Guid id)
        {
            Usuario _usuario = _repository.GetById(id);
            if (_usuario == null)
                throw new ApiException(ApiErrorCodes.INVUSU);

            _repository.Delete(_usuario);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public bool Edit(NomeBaseViewModel usuario, Guid id)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
                throw new ApiException(ApiErrorCodes.INVNOME, nameof(usuario.Nome));

            Usuario _usuario = _repository.GetById(id);

            if (_usuario == null)
                throw new ApiException(ApiErrorCodes.INVUSU);

            _usuario = _mapper.Map(usuario, _usuario);

            _repository.Update(_usuario);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERROPBD);

            return true;
        }

        public async Task<RetornoAutenticacaoViewModel> Authenticate(LoginViewModel autenticacao)
        {
            ValidationResult _validationResult = new LoginValidation().Validate(autenticacao);

            if (!_validationResult.IsValid)
                throw new ApiException(_validationResult.GetErrors(), ApiErrorCodes.MODNOTVALD);

            SignInResult _signInResult = await _signInManager.PasswordSignInAsync(autenticacao.Email, autenticacao.Senha, isPersistent: true, lockoutOnFailure: true);

            if (!_signInResult.Succeeded)
            {
                if (_signInResult.IsLockedOut)
                    throw new ApiException(ApiErrorCodes.LCKLOG);
                else if (_signInResult.IsNotAllowed)
                    throw new ApiException(ApiErrorCodes.NOTALLW);
                else
                    throw new ApiException(ApiErrorCodes.INVUSPASS);
            }

            Usuario _autenticacao = _repository.GetByUserName(
                                                        autenticacao.Email,
                                                        q => q
                                                            .Include(i => i.Perfil));

            string _generatedToken = GenerateTokenByAutenticacao(_autenticacao);

            return new RetornoAutenticacaoViewModel(token: _generatedToken,
                                                    isSenhaExpirada: _autenticacao.IsExpiredPassword(),
                                                    usuarioId: _autenticacao.Id);
        }

        public async Task AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha)
        {
            ContextUserDTO _loggedUser = _facadeAuth.GetLoggedUser();
            Usuario _autenticacao = _repository.GetByUserName(_loggedUser.UserName);

            if (!_autenticacao.IsExpiredPassword())
            {
                SignInResult _senhaAntigaResult = await _signInManager.CheckPasswordSignInAsync(_autenticacao, alterarSenha.SenhaAntiga, false);
                if (!_senhaAntigaResult.Succeeded)
                    throw new ApiException(ApiErrorCodes.SENANTINV, nameof(alterarSenha.SenhaAntiga));
            }

            await ValidatePassword(alterarSenha.NovaSenha, alterarSenha.ConfirmacaoNovaSenha, _autenticacao);

            IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(_autenticacao,
                                                                                    alterarSenha.SenhaAntiga,
                                                                                    alterarSenha.NovaSenha);
            _autenticacao.SetUnexpirablePassword();

            if (!changePasswordResult.Succeeded || !_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERALTSEIDNT);
        }

        public async Task RecuperarMinhaSenha(string email)
        {
            Usuario _autenticacao = _repository.GetByUserName(
                                                        email,
                                                        q => q
                                                            .Include(i => i.Tokens)
                                                        );

            /// <summary>
            /// Para prever segurança e privacidade para o proprietário do endereço de e-mail, 
            /// o usuário solicitante do "Recuperar senha" não será avisado que o e-mail não está registrado no sistema
            /// </summary>
            if (_autenticacao == null)
                return;

            string _identityToken = await _userManager.GeneratePasswordResetTokenAsync(_autenticacao);
            Token _newToken = _autenticacao.AddNewToken(_identityToken,
                                                        _repositoryTokenType.GetByCodigo(TokenType.ResetSenha).Id);

            //TODO: Enviar token por email para reset de senha

            _repository.Update(_autenticacao);

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERRGERTOK);
        }

        public async Task ResetSenha(ResetSenhaUsuarioViewModel resetSenha)
        {
            Token _token = _repositoryToken.GetById(Guid.Parse(resetSenha.Token));

            if (_token == null)
                throw new ApiException(ApiErrorCodes.INVTOK);

            await ValidatePassword(resetSenha.NovaSenha, resetSenha.ConfirmacaoNovaSenha, _token.Autenticacao);

            IdentityResult _resetSenhaResult = await _userManager.ResetPasswordAsync(_token.Autenticacao, _token.Value, resetSenha.NovaSenha);

            if (!_resetSenhaResult.Succeeded)
                throw new ApiException(ApiErrorCodes.INVTOK);

            _token.Autenticacao.SetUnexpirablePassword();

            if (!_uow.Commit())
                throw new ApiException(ApiErrorCodes.ERALTSEIDNT);
        }

        #region Métodos privados

        private async Task ValidatePassword(string novaSenha, string confirmacaoNovaSenha, Usuario autenticacao)
        {
            if (novaSenha != confirmacaoNovaSenha)
                throw new ApiException(ApiErrorCodes.SENDIV, nameof(confirmacaoNovaSenha));

            IdentityResult _validacaoSenhaResult = await _validatorPassword.ValidateAsync(_userManager, autenticacao, novaSenha);
            if (!_validacaoSenhaResult.Succeeded)
                throw new ApiException(ApiErrorCodes.SENINV, nameof(novaSenha));
        }

        private string GenerateTokenByAutenticacao(Usuario autenticacao)
        {
            ContextUserDTO _contextUser = new ContextUserDTO(id: autenticacao.Id.ToString(),
                                              userName: autenticacao.UserName,
                                              name: autenticacao.Nome,
                                              profile: string.Join(";", autenticacao.Perfil.Key));

            string _generatedToken = _facadeToken.GenerateToken(_contextUser);

            return _generatedToken;
        }

        #endregion
    }
}