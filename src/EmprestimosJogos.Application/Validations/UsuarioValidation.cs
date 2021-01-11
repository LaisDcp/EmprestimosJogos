using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation;

namespace EmprestimosJogos.Application.Validations
{
    public class UsuarioValidation : AbstractValidator<UsuarioViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public UsuarioValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage(ApiErrorCodes.INVEMAIL.GetDescription())
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);
        }
    }
}
