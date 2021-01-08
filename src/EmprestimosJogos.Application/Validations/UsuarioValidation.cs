using FluentValidation;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Application.ViewModels;

namespace EmprestimosJogos.Application.Validations
{
    public class UsuarioValidation : AbstractValidator<UsuarioViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public UsuarioValidation()
        {
            RuleFor(u => u.Email)
               .NotNull()
                   .WithMessage(_campoObrigatorioMessage)
               .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.Senha)
                .NotNull()
                    .WithMessage(_campoObrigatorioMessage)
                .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);
        }
    }
}
