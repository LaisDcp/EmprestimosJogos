using EmprestimosJogos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmprestimosJogos.Domain.Validations
{
    public class PasswordValidation : PasswordValidator<Usuario>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<Usuario> manager, Usuario user, string password)
        {
            IdentityResult baseValidatorResult = await base.ValidateAsync(manager, user, password);

            if (!baseValidatorResult.Succeeded)
                return baseValidatorResult;

            return await Task.FromResult(Regex.Match(password, @"[a-zA-Z]").Success
                                                                    ? IdentityResult.Success
                                                                    : IdentityResult.Failed(new IdentityError()));
        }
    }
}