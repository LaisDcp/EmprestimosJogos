namespace EmprestimosJogos.Application.ViewModels
{
    public class NomeBaseViewModel : EntityViewModel
    {
        public NomeBaseViewModel()
        {

        }

        public NomeBaseViewModel(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
