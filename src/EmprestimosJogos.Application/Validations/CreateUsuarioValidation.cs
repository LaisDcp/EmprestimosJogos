using FluentValidation;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using EmprestimosJogos.Application.ViewModels;

namespace EmprestimosJogos.Application.Validations
{
    public class CreateUsuarioValidation : AbstractValidator<CreateUsuarioViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public CreateUsuarioValidation()
        {
            RuleFor(u => u.Nome)
               .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.Email)
               .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.Senha)
                .NotEmpty()
                   .WithMessage(_campoObrigatorioMessage);
        }
    }
}
