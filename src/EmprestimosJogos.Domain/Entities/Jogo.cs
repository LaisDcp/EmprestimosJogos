using EmprestimosJogos.Domain.Core.Models;
using System;

namespace EmprestimosJogos.Domain.Entities
{
    public class Jogo : Entity
    {
        public Jogo()
        {
        }
        public string Nome { get; set; }

        public DateTime? DataUltimoEmprestimo { get; set; }

        public bool IsEmprestado { get; set; }

        public Guid? AmigoId { get; set; }

        public Guid CreatorId { get; set; }

        public virtual Usuario Creator { get; set; }

        public virtual Amigo Amigo { get; set; }
    }
}
