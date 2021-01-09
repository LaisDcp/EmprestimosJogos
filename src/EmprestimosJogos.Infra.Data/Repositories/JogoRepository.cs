using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public IQueryable<Jogo> AdvancedFilter(Expression<Func<Jogo, bool>> where, string sortBy, int page, int itemsPerPage)
        {
            return QueryPagedAndSortDynamic(where: where,
                                            includes: q => q
                                                      .Include(i => i.Amigo),
                                            select: sel => sel.Id,
                                            sortByProperty: sortBy,
                                            containsProperty: "Id",
                                            page: page,
                                            itemsPerPage: itemsPerPage);
        }

        public Jogo GetById(Guid id, Func<IQueryable<Jogo>, object> includes = null)
        {
            return Find(e => e.Id == id, includes);
        }
    }
}