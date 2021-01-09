using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using System;
using System.Linq;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public Usuario GetByUserName(string userName, Func<IQueryable<Usuario>, object> includes = null)
        {
            return Find(wh => wh.UserName == userName, includes);
        }

        public bool ExistsWithUserName(string userName)
        {
            return Query(wh => wh.Email == userName)
                    .Any();
        }

        public bool ExistsWithId(Guid id)
        {
            return Query(wh => wh.Id == id)
                    .Any();
        }

        public Usuario GetById(Guid id, Func<IQueryable<Usuario>, object> includes = null)
        {
            return Find(e => e.Id == id, includes);
        }
    }
}