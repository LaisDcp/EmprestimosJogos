using FluentValidation;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Infra.CrossCutting.Helpers;

namespace EmprestimosJogos.Domain.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public UsuarioValidation()
        {
            RuleFor(u => u.Email)
               .EmailAddress()
                   .WithMessage(ApiErrorCodes.INVEMAIL.GetDescription())
               .NotNull()
                   .WithMessage(_campoObrigatorioMessage);
        }
    }
}