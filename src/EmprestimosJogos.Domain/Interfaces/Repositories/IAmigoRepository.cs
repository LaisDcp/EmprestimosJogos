using EmprestimosJogos.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface IAmigoRepository : IRepository<Amigo>
    {
        IQueryable<Amigo> GetAll();

        Amigo GetById(Guid id, Func<IQueryable<Amigo>, object> includes = null);

        IQueryable<Amigo> AdvancedFilter(Expression<Func<Amigo, bool>> where, string sortyBy, int page, int itemsPerPage, Func<IQueryable<Amigo>, object> includes = null);

        bool ExistsWithId(Guid id);
    }
}
