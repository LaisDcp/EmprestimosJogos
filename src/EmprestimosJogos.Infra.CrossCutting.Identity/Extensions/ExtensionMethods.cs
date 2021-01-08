using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Text;

namespace EmprestimosJogos.Infra.CrossCutting.Identity.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converte todos os IdentityErrors em uma string só, separadada por espaços.
        /// </summary>
        /// <param name="identityResult"></param>
        /// <returns></returns>
        public static string GetErrorsToString(this IdentityResult identityResult)
        {
            StringBuilder _errors = new StringBuilder();

            identityResult.Errors?.ToList().ForEach(error =>
                _errors.Append($"{error.Description} "));

            return _errors.ToString();
        }
    }
}
