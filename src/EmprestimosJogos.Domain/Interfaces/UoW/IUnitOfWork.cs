using System;

namespace EmprestimosJogos.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Salva na base de dados todas as alterações no EFCore.Context
        /// </summary>
        /// <returns></returns>
        bool Commit();

        bool BeginTransaction();

        bool DisposeTransaction();

        bool CommitTransaction();
    }
}
