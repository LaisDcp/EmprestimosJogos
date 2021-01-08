using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(EmprestimosJogosContext context)
            : base(context)
        {
        }

        public Token GetById(Guid id)
        {
            return Find(wh => !wh.IsDeleted
                        && wh.Id == id
                        , q =>
                            q.Include(i => i.Autenticacao));
        }
    }
}