namespace EmprestimosJogos.Application.ViewModels
{
    public class CreateUsuarioViewModel : UsuarioViewModel
    {
        public CreateUsuarioViewModel()
        {

        }

        public CreateUsuarioViewModel(string nome, string email, string senha, string confirmacaoSenha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }

        public string Senha { get; set; }

        public string ConfirmacaoSenha { get; set; }
    }
}
