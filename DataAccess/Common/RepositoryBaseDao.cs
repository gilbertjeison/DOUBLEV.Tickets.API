using DataAccess.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Utilities.CustomModels;
using Utilities.ExtensionMethods;

namespace DataAccess.Common
{
    public abstract class RepositoryBaseDao<T> : BaseRepositoryDao<T>, IRepositoryBase<T>
        where T : class, new()
    {
        protected RepositoryBaseDao(IMainContext contexto) : base()
        {
            RepositoryContext = contexto;
        }

        public ICollection<T> ToList()
        {
            return ToList(null);
        }

        public ICollection<T> ToList(ParameterOfList<T> parameterOfList)
        {
            return ConfigureParameterOfList(RepositoryContext.Set<T>(), parameterOfList).ToList();
        }

        public T Search(Expression<Func<T, bool>> expression)
        {
            return this.Search(expression, null);
        }

        public T Search(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T objSearch = null;
            if (expression.IsNotNull())
            {
                objSearch = ConfigureIncludeSearch(RepositoryContext.Set<T>(), expression, includes).FirstOrDefault();
                EntityStateDetached(objSearch);
            }

            return objSearch;
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            long returnCount = 0;
            if (expression.IsNotNull())
            {
                returnCount = ConfigureIncludeSearch(RepositoryContext.Set<T>(), expression).LongCount();
            }

            return returnCount;
        }

        public Task<List<T>> ToListAsync()
        {
            return ToListAsync(null);
        }

        public Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList)
        {
            return ConfigureParameterOfList(RepositoryContext.Set<T>(), parameterOfList).ToListAsync();
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await this.SearchAsync(expression, null);
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T objSearch = null;
            if (expression.IsNotNull())
            {
                objSearch = await ConfigureIncludeSearch(RepositoryContext.Set<T>(), expression, includes).FirstOrDefaultAsync().ConfigureAwait(false);
            }

            return objSearch;
        }

        public async Task<int?> CreateAsync(T objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                RepositoryContext.Set<T>().Add(objCreate);
                returnCreate = await RepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnCreate;
        }

        public async Task<int?> CreateAsync(IEnumerable<T> objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                RepositoryContext.Set<T>().AddRange(objCreate);
                returnCreate = await RepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnCreate;
        }

        public async Task<bool?> EditAsync(T objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                EntityStateModified(objEdit);
                returnEdit = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnEdit;
        }

        public async Task<bool?> EditAsync(IEnumerable<T> objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                foreach (var item in objEdit)
                {
                    EntityStateModified(item);
                }

                returnEdit = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnEdit;
        }

        public async Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = ConfigureIncludeSearch(RepositoryContext.Set<T>(), expression).FirstOrDefault();
                returnDelete = await this.DeleteAsync(objDelete);
            }

            return returnDelete;
        }

        public async Task<bool?> DeleteAsync(T objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {
                RepositoryContext.Set<T>().Remove(objDelete);
                returnDelete = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnDelete;
        }

        public async Task<bool?> DeleteRangeAsync(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = ConfigureIncludeSearch(RepositoryContext.Set<T>(), expression).ToList();
                returnDelete = await this.DeleteRangeAsync(objDelete);
            }

            return returnDelete;
        }

        public async Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {
                RepositoryContext.Set<T>().RemoveRange(objDelete);
                returnDelete = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnDelete;
        }

        public CustomList<T> ToListPaged()
        {
            return ToListPaged(null);
        }

        public CustomList<T> ToListPaged(ParameterOfList<T> parameterOfList)
        {
            var oToList = new CustomList<T>(ConfigureParameterOfList(RepositoryContext.Set<T>(), parameterOfList));
            if (parameterOfList.IsNotNull())
            {
                oToList.Paged = parameterOfList.TextPag;
            }

            return oToList;
        }
    }
}
