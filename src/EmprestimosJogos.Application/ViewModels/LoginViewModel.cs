namespace EmprestimosJogos.Application.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {

        }

        public LoginViewModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
