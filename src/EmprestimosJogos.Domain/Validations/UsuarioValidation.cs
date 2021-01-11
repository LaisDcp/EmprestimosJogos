using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation;

namespace EmprestimosJogos.Domain.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
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

            RuleFor(u => u.PerfilId)
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);
        }
    }
}