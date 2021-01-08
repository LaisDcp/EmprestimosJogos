using EmprestimosJogos.Domain.Core.Models;
using System;

namespace EmprestimosJogos.Domain.Entities
{
    public class Token : Entity
    {
        public string Value { get; set; }

        public Guid TokenTypeId { get; set; }

        public Guid AutenticacaoId { get; set; }

        public Usuario Autenticacao { get; set; }

        public TokenType TokenType { get; set; }
    }
}