namespace EmprestimosJogos.Application.ViewModels
{
    public class FilterPaginacaoViewModel
    {
        public FilterPaginacaoViewModel()
        {
            IsDescending = false;
            Page = 0;
            ItemsPerPage = 1;
            SortBy = "Nome";
        }

        public string SortBy { get; set; }

        public bool IsDescending { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public string Descending()
        {
            return !IsDescending ? string.Empty : "descending";
        }
    }
}
