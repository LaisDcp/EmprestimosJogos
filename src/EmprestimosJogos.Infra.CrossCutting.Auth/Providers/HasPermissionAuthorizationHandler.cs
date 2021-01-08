using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{
    public class HasPermissionAuthorizationHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        private readonly IAuthFacade _facadeAuth;

        public HasPermissionAuthorizationHandler(IAuthFacade facadeAuth)
        {
            _facadeAuth = facadeAuth;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionRequirement requirement)
        {
            string _profile = requirement.Profile;

            ContextUserDTO _loggedUser = _facadeAuth.GetLoggedUser();

            if (string.IsNullOrEmpty(_profile) || _loggedUser.HasPermisson(_profile))
            {
                // Pode haver o mesmo attribute na Controller e nos métodos.
                // Então definimos o usuário logado Para que caso passe nesse mesmo
                // handler novamente não retorne Forbidden em um recurso já permitido.
                if (!_loggedUser.IsAllowedInHasPermissionPolicyChallenge)
                    _facadeAuth.SetLoggedUserAllowedInHasPermissionPolicyChallenge();

                context.Succeed(requirement);

                // Segue a pipeline de middlewares.
                return Task.CompletedTask;
            }

            // Como o middleware de ExceptionHandler foi registrado primeiro,
            // ao lançarmos a Exception, o response da request sairá no formato esperado.
            if (!_loggedUser.IsAuthenticated)
                throw new ApiException(ApiErrorCodes.NOTALLW);

            if (!context.PendingRequirements.Any(pending => pending.GetType() == typeof(HasPermissionRequirement)
                    && _loggedUser.HasPermisson(((HasPermissionRequirement)pending).Profile)))
            {
                context.Fail();
                throw new ApiException(ApiErrorCodes.NOTALLW);
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}