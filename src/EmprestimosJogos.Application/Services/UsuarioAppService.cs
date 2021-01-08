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
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmprestimosJogos.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenRepository _repositoryToken;
        private readonly ITokenTypeRepository _repositoryTokenType;

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
            _facadeToken = facadeToken;
            _facadeAuth = facadeAuth;
            _validatorPassword = validatorPassword;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<RetornoAutenticacaoViewModel> Authenticate(UsuarioViewModel autenticacao)
        {
            ValidationResult _validationResult = new UsuarioValidation().Validate(autenticacao);

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
            else
            {
                Usuario _autenticacao = _repository.GetByUserName(
                                                            autenticacao.Email,
                                                            q => q
                                                                .Include(i => i.Tokens));

                string _generatedToken = GenerateTokenByAutenticacao(_autenticacao);

                return GetRetornoAutenticacao(_autenticacao, _generatedToken);
            }
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

        /// <summary>
        /// Gerar um token a partir da Autenticacao
        /// </summary>
        /// <param name="autenticacao"></param>
        /// <param name="dispositivoId"></param>
        /// <returns></returns>
        private string GenerateTokenByAutenticacao(Usuario autenticacao)
        {
            ContextUserDTO _contextUser = new ContextUserDTO(id: autenticacao.Id.ToString(),
                                              userName: autenticacao.UserName,
                                              name: autenticacao.Nome,
                                              profile: string.Join(";", autenticacao.Perfil.Key));

            string _generatedToken = _facadeToken.GenerateToken(_contextUser);

            return _generatedToken;
        }

        /// <summary>
        /// Gerar a viewmodel de retorno da autenticação.
        /// </summary>
        /// <param name="autenticacao"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private RetornoAutenticacaoViewModel GetRetornoAutenticacao(Usuario autenticacao, string token)
        {
            return new RetornoAutenticacaoViewModel(token: token,
                                                      isSenhaExpirada: autenticacao.IsExpiredPassword(),
                                                      usuarioId: autenticacao.Id);
        }

        #endregion
    }
}