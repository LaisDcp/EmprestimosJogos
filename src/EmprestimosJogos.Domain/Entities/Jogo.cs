using EmprestimosJogos.Domain.Core.Models;
using System;

namespace EmprestimosJogos.Domain.Entities
{
    public class Jogo : Entity
    {
        public Jogo()
        {
            IsEmprestado = false;
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
            DataEmprestimo = DateTime.Now;
            IsEmprestado = true;
            Amigo = null;
            AmigoId = null;
        }

        public void Emprestar(Guid amigoId)
        {
            DataEmprestimo = null;
            IsEmprestado = false;
            AmigoId = amigoId;
        }
    }
}
