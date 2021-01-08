using EmprestimosJogos.Application.ViewModels;
using System.Threading.Tasks;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<RetornoAutenticacaoViewModel> Authenticate(UsuarioViewModel autenticacao);

        Task AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha);

        Task ResetSenha(ResetSenhaUsuarioViewModel resetSenha);

        Task RecuperarMinhaSenha(string email);
    }
}
