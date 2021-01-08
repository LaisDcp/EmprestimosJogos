using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class TokenTypeRepository : Repository<TokenType>, ITokenTypeRepository
    {
        public TokenTypeRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public TokenType GetByCodigo(string codigo)
        {
            return Find(p => !p.IsDeleted
                            && p.Key == codigo);
        }
    }
}