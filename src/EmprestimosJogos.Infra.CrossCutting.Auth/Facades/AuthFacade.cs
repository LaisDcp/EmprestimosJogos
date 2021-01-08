using EmprestimosJogos.Domain.Core.DTO;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Core.Types;
using EmprestimosJogos.Domain.Interfaces.Facades;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Facades
{
    public class AuthFacade : IAuthFacade
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthFacade(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ContextUserDTO GetLoggedUser()
        {
            if (_httpContextAccessor.HttpContext?.User == null)
                return null;

            IEnumerable<Claim> _claims = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims;

            ContextUserDTO context = new ContextUserDTO
            {
                Id = _claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value ?? string.Empty,
                UserName = _claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = _claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty,
                Profile = _claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty,
                IsAllowedInHasPermissionPolicyChallenge = bool.TryParse(_claims.FirstOrDefault(x => x.Type == CustomClaimTypes.IsAllowedInHasPermissionPolicyChallenge)?.Value ?? string.Empty, out bool value) && value,
                IsAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated,
                OriginalUserName = _claims.FirstOrDefault(x => x.Type == CustomClaimTypes.OriginalUserName)?.Value ?? string.Empty,
            };
            return context;
        }

        public ClaimsIdentity GetClaimsIdentityByContextUser(ContextUserDTO contextUser, string authenticationType = "Bearer")
        {
            ClaimsIdentity _claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.PrimarySid, contextUser.Id ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, contextUser.UserName ?? string.Empty),
                new Claim(ClaimTypes.Name, contextUser.Name ?? string.Empty),
                new Claim(ClaimTypes.Role, contextUser.Profile ?? string.Empty),
                new Claim(CustomClaimTypes.IsAllowedInHasPermissionPolicyChallenge, contextUser.IsAllowedInHasPermissionPolicyChallenge.ToString()),
                new Claim(CustomClaimTypes.OriginalUserName, contextUser.OriginalUserName ?? string.Empty),
            }, authenticationType);

            return _claims;
        }

        public void SetUserToCurrentHTTPContext(ContextUserDTO user, string authenticationType = null)
        {
            if (user == null)
                throw new ApiException(ApiErrorCodes.INVUSU);

            _httpContextAccessor.HttpContext.User =
                new GenericPrincipal(GetClaimsIdentityByContextUser(user, authenticationType), new string[] { "" });
        }

        public void SetLoggedUserAllowedInHasPermissionPolicyChallenge()
        {
            ContextUserDTO _loggedUser = GetLoggedUser();
            _loggedUser.IsAllowedInHasPermissionPolicyChallenge = true;

            SetUserToCurrentHTTPContext(_loggedUser);
        }
    }
}
