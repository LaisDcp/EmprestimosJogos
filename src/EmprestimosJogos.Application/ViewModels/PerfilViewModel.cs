using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class PerfilViewModel : EntityViewModel
    {
        public Guid? PerfilParentId { get; set; }

        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Icone { get; set; }
    }
}

