using EmprestimosJogos.Domain.Entities;
using System;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface ITokenRepository : IRepository<Token>
    {
        Token GetById(Guid id);
    }
}
