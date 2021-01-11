using EmprestimosJogos.Domain.Core.Models;
using EmprestimosJogos.Domain.Validations;
using System;

namespace EmprestimosJogos.Domain.Entities
{
    public class Jogo : Entity
    {
        public Jogo()
        {
            IsEmprestado = false;
        }

        public Jogo(string nome, Guid creatorId, Guid id) : base()
        {
            Nome = nome;
            CreatorId = creatorId;
            Id = id;
        }

        public string Nome { get; set; }

        public DateTime? DataEmprestimo { get; set; }

        public bool IsEmprestado { get; set; }

        public Guid? AmigoId { get; set; }

        public Guid CreatorId { get; set; }

        public virtual Usuario Creator { get; set; }

        public virtual Amigo Amigo { get; set; }

        public void SetCreatorId(Guid creatorId)
        {
            CreatorId = creatorId;
        }

        public void Devolver()
        {
            DataEmprestimo = null;
            IsEmprestado = false;
            Amigo = null;
            AmigoId = null;
        }

        public void Emprestar(Guid amigoId)
        {
            DataEmprestimo = DateTime.Now;
            IsEmprestado = true;
            AmigoId = amigoId;
        }

        public bool IsValid()
        {
            ValidationResult = new JogoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
