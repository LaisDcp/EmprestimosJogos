using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class FilterUsuarioViewModel : FilterPaginacaoViewModel
    {
        public string ContainsProperties { get; set; }

        public PerfilViewModel Perfil { get; set; }

        public Guid? EscolaId { get; set; }
    }
}
