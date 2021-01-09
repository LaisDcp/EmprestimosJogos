using EmprestimosJogos.Domain.Core.Models;
using EmprestimosJogos.Domain.Validations;
using System;
using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Entities
{
    public class Amigo : Entity
    {
        public Amigo()
        {
            Jogos = new List<Jogo>();
        }

        public string Nome { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public int? Numero { get; set; }

        public string TelefoneCelular { get; set; }

        public Guid CreatorId { get; set; }

        public virtual Usuario Creator { get; set; }

        public virtual ICollection<Jogo> Jogos { get; set; }

        public bool IsValid()
        {
            ValidationResult = new AmigoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void SetCreatorId(Guid creatorId)
        {
            CreatorId = creatorId;
        }
    }
}
