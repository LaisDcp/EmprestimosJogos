using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation;

namespace EmprestimosJogos.Application.Validations
{
    public class AmigoValidation : AbstractValidator<AmigoViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();
        public AmigoValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.CEP)
                .Length(8)
                .When(u => !string.IsNullOrEmpty(u.CEP))
                    .WithMessage(ApiErrorCodes.INVCEP.GetDescription());

            RuleFor(u => u.TelefoneCelular)
                .Length(11)
                    .WithMessage(ApiErrorCodes.INVCEL.GetDescription())
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);
        }
    }
}