using EmprestimosJogos.Domain.Entities;
using System;

namespace EmprestimosJogos.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository : IRepository<Perfil>
    {
        Perfil GetById(Guid id);

        Perfil GetByKey(string key);
    }
}
