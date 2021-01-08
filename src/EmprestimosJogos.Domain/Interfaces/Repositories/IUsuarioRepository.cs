using EmprestimosJogos.Domain.Entities;
using System;
using System.Linq;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetByUserName(string userName, Func<IQueryable<Usuario>, object> includes = null);

        bool ExistsWithUserName(string userName);
    }
}
