using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Domain.Interfaces.UoW;
using EmprestimosJogos.Infra.CrossCutting.Auth.Facades;
using EmprestimosJogos.Infra.Data.Context;
using EmprestimosJogos.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EmprestimosJogos.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<EmprestimosJogosContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Facades
            services.AddSingleton<IAuthFacade, AuthFacade>();
            services.AddScoped<ITokenFacade, TokenFacade>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region uow
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region AppServices

            #endregion

            #region Repositories

            #endregion
        }
    }
}