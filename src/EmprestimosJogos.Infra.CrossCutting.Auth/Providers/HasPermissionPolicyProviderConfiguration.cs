using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{
    public static class CustomPolicyProviderConfiguration
    {
        public static IServiceCollection AddCustomPolicyProviderConfiguration(this IServiceCollection services)
        {
            // Sobrescreva o authorization policy provider padrão pelo nosso
            // custom provider que possa retornar policies para determinado se
            // baseando nos nomes de política (em vez de usar o provedor de política padrão)
            services.AddSingleton<IAuthorizationPolicyProvider, HasPermissionPolicyProvider>();

            // Como sempre, os handlers devem ser fornecidos para os requisitos das policies de autorização
            services.AddSingleton<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

            return services;
        }
    }
}
