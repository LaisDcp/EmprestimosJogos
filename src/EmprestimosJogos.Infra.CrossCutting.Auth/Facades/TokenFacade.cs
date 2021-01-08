using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Interfaces.Facades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Facades
{
    public class TokenFacade : ITokenFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthFacade _serviceAuth;

        public TokenFacade(IConfiguration configuration,
                           IAuthFacade serviceAuth)
        {
            _configuration = configuration;
            _serviceAuth = serviceAuth;
        }

        public string GenerateToken(ContextUserDTO user, double? minutesExpiration = null)
        {
            try
            {
                TokenConfigurationsDTO _tokenConfigurations =
                        _configuration.GetSection("TokenConfigurations")
                            ?.Get<TokenConfigurationsDTO>();

                if (_tokenConfigurations == null)
                    throw new ArgumentNullException("Não foi possível encontrar as configurações do Token JWT.");

                JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
                byte[] _key = Encoding.ASCII.GetBytes(_tokenConfigurations.Secret);

                SecurityTokenDescriptor _tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = _serviceAuth.GetClaimsIdentityByContextUser(user),
                    Expires = DateTime.UtcNow.AddMinutes(minutesExpiration ?? _tokenConfigurations.ExpiresIn),
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(
                                                new SymmetricSecurityKey(_key),
                                                    SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken _generatedToken = _tokenHandler.CreateToken(_tokenDescriptor);

                return _tokenHandler.WriteToken(_generatedToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
