using EmprestimosJogos.Domain.Core.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{
    public static class JWTConfiguration
    {
        public static IServiceCollection AddCustomJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                TokenConfigurationsDTO _tokenConfigurations =
                        configuration.GetSection("TokenConfigurations")
                            ?.Get<TokenConfigurationsDTO>();

                if (_tokenConfigurations == null)
                    throw new ArgumentNullException("Não foi possível encontrar as configurações do Token JWT.");

                byte[] _key = Encoding.ASCII.GetBytes(_tokenConfigurations.Secret);

                services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(_key),
                            ValidateIssuer = false, //caso o servidor de autorização seja diferente
                            ValidateAudience = false, //seria o servidor que vai fazer o request da autorização pro Issuer
                            RequireExpirationTime = true
                        };

                        x.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                context.Request.Headers.TryGetValue("Authorization", out StringValues accessToken);

                                string _path = context.HttpContext.Request.Path.ToString();

                                if (!string.IsNullOrEmpty(accessToken) &&
                                    _path.Contains("/notificacoes-logged-user"))
                                {
                                    if (accessToken.ToString().Contains("Bearer"))
                                        accessToken = accessToken.ToString().Split(' ')[1];

                                    context.Token = accessToken;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });

                return services;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
