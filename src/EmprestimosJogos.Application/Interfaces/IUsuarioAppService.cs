using EmprestimosJogos.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        UsuarioViewModel GetById(Guid id);

        Task<bool> CreateAsync(CreateUsuarioViewModel usuario);

        bool Delete(Guid id);

        bool Edit(NomeBaseViewModel usuario, Guid id);

        Task<RetornoAutenticacaoViewModel> Authenticate(LoginViewModel autenticacao);

        Task AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha);

        Task ResetSenha(ResetSenhaUsuarioViewModel resetSenha);

        Task<TokenViewModel> RecuperarMinhaSenha(string email);
    }
}
