using EmprestimosJogos.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Jogo GetById(Guid id, Func<IQueryable<Jogo>, object> includes = null);

        IQueryable<Jogo> AdvancedFilter(Expression<Func<Jogo, bool>> where, string sortBy, int page, int itemsPerPage);
    }
}
