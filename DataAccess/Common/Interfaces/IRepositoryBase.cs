using System.Linq.Expressions;
using Utilities.CustomModels;

namespace DataAccess.Common.Interfaces
{
    public interface IRepositoryBase<T>
        where T : class, new()
    {
        IMainContext RepositoryContext { get; }

        void SetAutoSave(bool value);

        T Search(Expression<Func<T, bool>> expression);

        long Count(Expression<Func<T, bool>> expression);

        void SetParameterBusinessRules(ParameterBusinessRules parameterBusinessRules);

        Task<List<T>> ToListAsync();

        Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList);

        Task<T> SearchAsync(Expression<Func<T, bool>> expression);

        Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        Task<int?> CreateAsync(T objCreate);

        Task<int?> CreateAsync(IEnumerable<T> objCreate);

        Task<bool?> EditAsync(T objEdit);

        Task<bool?> EditAsync(IEnumerable<T> objEdit);

        Task<bool?> DeleteAsync(T objDelete);

        Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression);

        Task<bool?> DeleteRangeAsync(Expression<Func<T, bool>> expression);

        Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete);

        CustomList<T> ToListPaged();

        CustomList<T> ToListPaged(ParameterOfList<T> parameterOfList);
    }
}
