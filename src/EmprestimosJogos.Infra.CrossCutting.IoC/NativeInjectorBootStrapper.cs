using EmprestimosJogos.Application.Interfaces;
using EmprestimosJogos.Application.Services;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Domain.Interfaces.UoW;
using EmprestimosJogos.Domain.Validations;
using EmprestimosJogos.Infra.CrossCutting.Auth.Facades;
using EmprestimosJogos.Infra.Data.Context;
using EmprestimosJogos.Infra.Data.Repositories;
using EmprestimosJogos.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EmprestimosJogos.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<EmprestimosJogosContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPasswordValidator<Usuario>, PasswordValidation>();

            #region Facades
            services.AddSingleton<IAuthFacade, AuthFacade>();
            services.AddScoped<ITokenFacade, TokenFacade>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region uow
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region AppServices

            services.AddScoped<IAmigoAppService, AmigoAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            #endregion

            #region Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenTypeRepository, TokenTypeRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            #endregion
        }
    }
}