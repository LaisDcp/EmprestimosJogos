using FluentValidation;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Application.ViewModels;

namespace EmprestimosJogos.Application.Validations
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public LoginValidation()
        {
            RuleFor(u => u.Email)
               .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.Senha)
                .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);
        }
    }
}
