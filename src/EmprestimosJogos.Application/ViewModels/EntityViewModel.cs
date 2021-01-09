using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class EntityViewModel
    {
        public EntityViewModel() { }

        public EntityViewModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}