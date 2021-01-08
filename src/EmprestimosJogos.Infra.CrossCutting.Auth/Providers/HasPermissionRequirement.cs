using Microsoft.AspNetCore.Authorization;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{

    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public const string HAS_PERMISSION_POLICY_PREFIX = "HasPermission";

        public string Profile { get; }

        public HasPermissionRequirement(string profile)
        {
            Profile = profile;
        }
    }
}
