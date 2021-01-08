using EmprestimosJogos.Domain.Entities;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface ITokenTypeRepository : IRepository<TokenType>
    {
        TokenType GetByCodigo(string codigo);
    }
}
