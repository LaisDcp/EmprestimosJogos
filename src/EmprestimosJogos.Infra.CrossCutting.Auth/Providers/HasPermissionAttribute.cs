using Microsoft.AspNetCore.Authorization;

namespace EmprestimosJogos.Infra.CrossCutting.Auth.Providers
{

    // Este atributo deriva do atributo [Authorize], recebendo o param 'Permission'.
    // Na pipeline de middlewares, as policies são pesquisadas no policy providers apenas por string,
    // Com isso, caso o atributo tenha o prefixo esperado, é então criada uma policy cujo nome será
    // prefixo_constante + permission param. O responsável por criar a policy customizada é
    // é o provedor de autorização customizado (`HasPermissionPolicyProvider`).
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        private const string _authenticationType = "JWT";

        public HasPermissionAttribute(string module = null, string action = null) => Permission = $"{_authenticationType}:{module}:{action}";

        // Get or set the Permission property by manipulating the underlying Policy property
        public string Permission
        {
            get => Policy.Substring(HasPermissionRequirement.HAS_PERMISSION_POLICY_PREFIX.Length);
            set => Policy = $"{HasPermissionRequirement.HAS_PERMISSION_POLICY_PREFIX}{value}";
        }
    }
}
