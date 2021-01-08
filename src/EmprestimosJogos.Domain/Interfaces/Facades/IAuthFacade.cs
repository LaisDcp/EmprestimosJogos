using EmprestimosJogos.Domain.Core.DTO;
using System.Security.Claims;

namespace EmprestimosJogos.Domain.Interfaces.Facades
{
    public interface IAuthFacade
    {
        /// <summary>
        /// Obtém o usuário do HTTPContext atual através dos dados dos dados de ClaimsPrincipal.
        /// </summary>
        /// <returns></returns>
        ContextUserDTO GetLoggedUser();

        ClaimsIdentity GetClaimsIdentityByContextUser(ContextUserDTO user, string authenticationType = "Bearer");

        void SetUserToCurrentHTTPContext(ContextUserDTO user, string authenticationType = null);

        void SetLoggedUserAllowedInHasPermissionPolicyChallenge();
    }
}
