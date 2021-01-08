using EmprestimosJogos.Domain.Core.Models;
using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Entities
{
    public class Perfil : Entity
    {
        public const string ADM = "ADM";

        public Perfil()
        {
            Usuarios = new List<Usuario>();
        }

        public string Key { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string NormalizedRoleName { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}