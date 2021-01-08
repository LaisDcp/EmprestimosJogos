using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmprestimosJogos.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        #region 'Métodos Criar/Atualizar/Excluir/Salvar'

        TEntity Create(TEntity model);

        List<TEntity> Create(List<TEntity> model);

        bool UpdateAll(TEntity model);

        bool Update(TEntity model);

        bool Update(List<TEntity> model);

        bool Delete(TEntity model);

        bool Delete(List<TEntity> models);

        bool Delete(params object[] Keys);

        bool Delete(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 'Métodos de Busca'

        IQueryable<TEntity> QueryPagedAndSortDynamic<TType>(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, object> includes, Expression<Func<TEntity, TType>> select, string sortyByProperty, string containsProperty, int page, int itensPerPage);

        IQueryable<TEntity> QueryPagedAndSortDynamic<TType, TOrder>(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, object> includes, Expression<Func<TEntity, TType>> select, string sortyByProperty, string containsProperty, int page, int itensPerPage, Expression<Func<TEntity, TOrder>> orderBy = null, bool isDescending = false);

        TEntity Find(params object[] Keys);

        TEntity Find(Expression<Func<TEntity, bool>> where);

        TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

        TEntity FindAsTracking(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes);

        #endregion

        #region 'Metodos Assíncronos'

        #region 'Métodos Criar/Atualizar/Excluir/Salvar'

        Task<TEntity> CreateAsync(TEntity model);

        Task<bool> UpdateAsync(TEntity model);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where);

        Task<bool> DeleteAsync(TEntity model);

        Task<bool> DeleteAsync(params object[] Keys);

        Task<int> SaveAsync();

        #endregion

        #region 'Métodos de Busca'

        Task<TEntity> GetAsync(params object[] Keys);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #endregion
    }
}
