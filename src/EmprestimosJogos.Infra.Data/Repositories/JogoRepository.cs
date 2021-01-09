using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using System;
using System.Linq;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public Jogo GetById(Guid id, Func<IQueryable<Jogo>, object> includes = null)
        {
            return Find(e => e.Id == id, includes);
        }
    }
}