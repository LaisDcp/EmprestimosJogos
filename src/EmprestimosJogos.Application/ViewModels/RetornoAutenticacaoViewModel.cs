using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class RetornoAutenticacaoViewModel
    {
        public RetornoAutenticacaoViewModel() { }

        public RetornoAutenticacaoViewModel(string token, bool isSenhaExpirada, Guid usuarioId)
        {
            Token = token;
            IsSenhaExpirada = isSenhaExpirada;
            UsuarioId = usuarioId;
        }

        public RetornoAutenticacaoViewModel(string token, bool isSenhaExpirada)
        {
            Token = token;
            IsSenhaExpirada = isSenhaExpirada;
        }

        public Guid UsuarioId { get; set; }

        public string Token { get; set; }

        public bool IsSenhaExpirada { get; set; }

    }
}