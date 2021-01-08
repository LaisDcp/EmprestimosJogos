using EmprestimosJogos.Infra.CrossCutting.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmprestimosJogos.Infra.CrossCutting.Identity.Providers
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddCustomIdentityConfiguration(this IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Informar identidade para usar nossas classes personalizadas para usuários e perfis
            services.AddIdentity<Usuario, Perfil>()
                    .AddDefaultTokenProviders();

            //Diga à identidade para usar nosso provedor de armazenamento personalizado para usuários
            services.AddTransient<IUserStore<Usuario>, UsuarioStore>();

            //Diga à identidade para usar nosso provedor de armazenamento personalizado para perfis
            services.AddTransient<IRoleStore<Perfil>, PerfilStore>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

            });

            return services;
        }
    }
}
