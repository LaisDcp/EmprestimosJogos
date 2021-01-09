﻿using EmprestimosJogos.Domain.Core.Enum;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Infra.CrossCutting.Helpers;
using FluentValidation;

namespace EmprestimosJogos.Domain.Validations
{
    public class AmigoValidation : AbstractValidator<Amigo>
    {
        private string _campoObrigatorioMessage = ApiErrorCodes.CAMPOBRG.GetDescription();

        public AmigoValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                    .WithMessage(_campoObrigatorioMessage);

            RuleFor(u => u.CEP)
                .MinimumLength(8)
                .When(u => !string.IsNullOrEmpty(u.CEP))
                    .WithMessage(ApiErrorCodes.INVCEP.GetDescription());

            RuleFor(u => u.TelefoneCelular)
                .MinimumLength(10)
                .When(u => !string.IsNullOrEmpty(u.TelefoneCelular))
                    .WithMessage(ApiErrorCodes.INVCEL.GetDescription());
        }
    }
}