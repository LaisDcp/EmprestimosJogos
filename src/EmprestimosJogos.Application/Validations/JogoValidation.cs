using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation;

namespace EmprestimosJogos.Application.Validations
{
    public class JogoValidation : AbstractValidator<JogoViewModel>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();
        public JogoValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);
        }
    }
}