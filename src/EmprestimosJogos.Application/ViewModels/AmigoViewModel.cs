namespace EmprestimosJogos.Application.ViewModels
{
    public class AmigoViewModel : EntityViewModel
    {
        public AmigoViewModel()
        {
            
        }
        public string Nome { get; set; }

        public string CEP { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public int Numero { get; set; }

        public string Logradouro { get; set; }

        public string TelefoneCelular { get; set; }
    }
}