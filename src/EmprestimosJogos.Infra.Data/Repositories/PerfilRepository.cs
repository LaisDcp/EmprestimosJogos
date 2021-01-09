using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using System;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class PerfilRepository : Repository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public Perfil GetById(Guid id)
        {
            return Find(p => p.Id == id);
        }

        public Perfil GetByKey(string key)
        {
            return Find(p => p.Key == key);
        }

    }
}