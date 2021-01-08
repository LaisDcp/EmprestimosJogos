using EmprestimosJogos.Domain.Interfaces.UoW;
using EmprestimosJogos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace EmprestimosJogos.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmprestimosJogosContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(EmprestimosJogosContext context)
        {
            _context = context;
        }

        public bool BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();

            return true;
        }

        public bool Commit()
        {
            try
            {
                int _commited = _context.SaveChanges();
                return _commited > 0;
            }
            catch (Exception ex)
            {
                _context.ResetContextState();
                throw ex;
            }
        }

        public bool CommitTransaction()
        {
            if (_transaction == null)
                return false;

            _transaction.Commit();

            return true;
        }

        public bool DisposeTransaction()
        {
            if (_transaction == null)
                return false;

            _transaction.Dispose();

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
