using EmprestimosJogos.Domain.Core.Models;
using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Entities
{
    public class TokenType : Entity
    {
        public const string ResetSenha = "RESE";

        public TokenType()
        {
            Tokens = new List<Token>();
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}