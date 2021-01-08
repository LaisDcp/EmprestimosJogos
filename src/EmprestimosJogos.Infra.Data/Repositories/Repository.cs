using EmprestimosJogos.Domain.Core.Models;
using EmprestimosJogos.Domain.Interfaces;
using EmprestimosJogos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region 'Propriedades'

        protected readonly EmprestimosJogosContext _context;

        public DbSet<TEntity> _dbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }


        #endregion

        public Repository(EmprestimosJogosContext context)
        {
            _context = context;
        }

        #region 'Métodos Criar/Atualizar/Excluir/Salvar'

        public TEntity Create(TEntity model)
        {
            try
            {
                _dbSet.Add(model);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> Create(List<TEntity> models)
        {
            try
            {
                _dbSet.AddRange(models);
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAll(TEntity model)
        {
            try
            {
                _dbSet.Update(model);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                _dbSet.Attach(model);

                entry.State = EntityState.Modified;

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(List<TEntity> models)
        {
            try
            {
                foreach (var registro in models)
                {
                    var entry = _context.Entry(registro);
                    _dbSet.Attach(registro);
                    entry.State = EntityState.Modified;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Caso haja compatibilidade, onde TEntity herde de 'Entity',
        /// o campo 'IsDeleted' será definido para true automaticamente,
        /// utilizando a exclusão lógica, como uma das regras internas de sistemas.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(TEntity model)
        {
            try
            {
                if (model is Entity entity)
                {
                    entity.IsDeleted = true;
                    var _entry = _context.Entry(model);

                    _dbSet.Attach(model);

                    _entry.State = EntityState.Modified;
                }
                else
                {
                    var _entry = _context.Entry(model);
                    _dbSet.Attach(model);
                    _entry.State = EntityState.Deleted;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(List<TEntity> models)
        {
            try
            {
                foreach (var registro in models)
                {
                    Delete(registro);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var model = _dbSet.Find(id);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(params object[] Keys)
        {
            try
            {
                var model = _dbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = _dbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Métodos Busca'

        public IQueryable<TEntity> QueryPagedAndSortDynamic<TType>(
                                                Expression<Func<TEntity, bool>> where,
                                                Func<IQueryable<TEntity>, object> includes,
                                                Expression<Func<TEntity, TType>> select,
                                                string sortByProperty,
                                                string containsProperty,
                                                int page,
                                                int itemsPerPage)
        {
            return QueryPagedAndSortDynamic<TType, string>(where, includes, select, sortByProperty, containsProperty, page, itemsPerPage);
        }

        public IQueryable<TEntity> QueryPagedAndSortDynamic<TType, TOrder>(
                                                        Expression<Func<TEntity, bool>> where,
                                                        Func<IQueryable<TEntity>, object> includes,
                                                        Expression<Func<TEntity, TType>> select,
                                                        string sortByProperty,
                                                        string containsProperty,
                                                        int page,
                                                        int itemsPerPage,
                                                        Expression<Func<TEntity, TOrder>> orderBy = null,
                                                        bool isDescending = false)
        {
            try
            {
                IQueryable<TEntity> _filtered = _dbSet.Where(where);

                if (orderBy != null)
                {
                    if (isDescending)
                        _filtered = _filtered.OrderByDescending(orderBy);
                    else
                        _filtered = _filtered.OrderBy(orderBy);
                }
                else
                    _filtered = _filtered.OrderBy(sortByProperty);

                IQueryable<TType> _pagedProperty = _filtered.Select(select)
                    .Skip(page * itemsPerPage)
                    .Take(itemsPerPage);

                IQueryable<TEntity> _query = _dbSet.Where(wh => _pagedProperty.Contains(EF.Property<TType>(wh, containsProperty)));

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                if (orderBy != null)
                {
                    if (isDescending)
                        return _query.Where(where).OrderByDescending(orderBy);
                    else
                        return _query.Where(where).OrderBy(orderBy);
                }
                else
                    return _query.Where(where).OrderBy(sortByProperty);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(params object[] Keys)
        {
            try
            {
                return _dbSet.Find(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _dbSet.SingleOrDefault(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = _dbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TEntity FindAsTracking(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = _dbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.AsTracking().SingleOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return _dbSet.Where(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = _dbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.Where(predicate).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Métodos Assíncronos'

        #region 'Métodos Criar/Atualizar/Excluir/Salvar'

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                _dbSet.Add(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                _dbSet.Attach(model);

                entry.State = EntityState.Modified;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            try
            {
                var entry = _context.Entry(model);

                _dbSet.Attach(model);

                entry.State = EntityState.Deleted;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            try
            {
                var model = _dbSet.Find(Keys);
                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var model = _dbSet.FirstOrDefault(where);

                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 'Métodos Busca'

        public async Task<TEntity> GetAsync(params object[] Keys)
        {
            try
            {
                return await _dbSet.FindAsync(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion

        public void Dispose()
        {
            try
            {
                if (_context != null)
                    _context.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}