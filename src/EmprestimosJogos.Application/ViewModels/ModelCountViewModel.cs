using System.Collections.Generic;

namespace EmprestimosJogos.Application.ViewModels
{
    public class ModelCountViewModel<TType> where TType : class
    {
        public ModelCountViewModel()
        {
            Items = new List<TType>();
        }

        public ModelCountViewModel(List<TType> itens)
        {
            Items = itens;
        }

        public ModelCountViewModel(List<TType> itens, int count)
        {
            Items = itens;
            Count = count;
        }

        public List<TType> Items { get; set; }

        public int Count { get; set; }

        public bool HasPagination
        {
            get
            {
                return Count > Items.Count;
            }
        }
    }
}
