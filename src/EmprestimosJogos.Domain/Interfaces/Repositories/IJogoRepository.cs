using EmprestimosJogos.Domain.Entities;
using System;
using System.Linq;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Jogo GetById(Guid id, Func<IQueryable<Jogo>, object> includes = null);
    }
}
