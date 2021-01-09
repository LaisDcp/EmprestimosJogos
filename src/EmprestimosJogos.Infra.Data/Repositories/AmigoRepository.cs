using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class AmigoRepository : Repository<Amigo>, IAmigoRepository
    {
        public AmigoRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public IQueryable<Amigo> GetAll()
        {
            return Query();
        }

        public Amigo GetById(Guid id, Func<IQueryable<Amigo>, object> includes = null)
        {
            return Find(wh => wh.Id == id,
                        includes);
        }

        public IQueryable<Amigo> AdvancedFilter(Expression<Func<Amigo, bool>> where, string sortBy, int page, int itemsPerPage, Func<IQueryable<Amigo>, object> includes = null)
        {
            return QueryPagedAndSortDynamic(where: where,
                                            includes: includes,
                                            select: sel => sel.Id,
                                            sortByProperty: sortBy,
                                            containsProperty: "Id",
                                            page: page,
                                            itemsPerPage: itemsPerPage);
        }

        public bool ExistsWithId(Guid id)
           => Query(c => c.Id == id).Any();

    }
}