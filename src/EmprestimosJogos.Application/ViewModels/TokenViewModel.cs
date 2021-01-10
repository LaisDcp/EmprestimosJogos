using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class TokenViewModel
    {
        public TokenViewModel()
        { }

        public TokenViewModel(Guid tokenId)
        {
            TokenId = tokenId;
        }

        public Guid TokenId { get; set; }
    }
}