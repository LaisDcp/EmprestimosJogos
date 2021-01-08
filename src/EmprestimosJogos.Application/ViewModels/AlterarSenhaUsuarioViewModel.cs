namespace EmprestimosJogos.Application.ViewModels
{
    public class AlterarSenhaUsuarioViewModel
    {
        public string Email { get; set; }

        public string SenhaAntiga { get; set; }

        public string NovaSenha { get; set; }

        public string ConfirmacaoNovaSenha { get; set; }
    }
}