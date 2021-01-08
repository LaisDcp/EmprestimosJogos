using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{
    public class HasPermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public HasPermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // Cria um policy provider nativo para ser usado caso não haja um handler para a policy.
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyNameFromAttribute)
        {
            string _suffix = policyNameFromAttribute.Substring(HasPermissionRequirement.HAS_PERMISSION_POLICY_PREFIX.Length);

            string _profileCodeFromAttribute = _suffix.Split(":")[1];

            return Task.FromResult(
                    new AuthorizationPolicyBuilder()
                        .AddRequirements(
                            new HasPermissionRequirement(_profileCodeFromAttribute))
                        .Build());
        }

    }
}