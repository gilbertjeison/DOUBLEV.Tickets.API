using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Utilities.CustomModels;
using Utilities.ExtensionMethods;

namespace DataAccess.Common
{
    public partial class BaseRepositoryDao<T>
          where T : class, new()
    {
        protected IQueryable<T> ConfigureParameterOfList(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            IQuery = WheresTransversal(IQuery);

            if (parameterOfList.IsNotNull())
            {

                IQuery = ConfigureOrderBy(IQuery, parameterOfList);
                IQuery = ConfigureFilter(IQuery, parameterOfList);
                IQuery = ConfigureInclude(IQuery, parameterOfList);

                IQuery = ConfigureMaxCount(IQuery, parameterOfList);
                IQuery = ConfigurePaged(IQuery, parameterOfList);
            }

            return IQuery;
        }

        protected IQueryable<T> ConfigureIncludeSearch(IQueryable<T> IQuery, Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            if (includes.IsNotNull())
            {
                IQuery = includes.Aggregate(IQuery,
                                (current, include) => current.Include(include));
            }

            IQuery = IQuery.Where(expression);
            IQuery = this.WheresTransversal(IQuery);

            return IQuery;
        }

        private static IQueryable<T> ConfigureOrderBy(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.OrderByDynamic.IsNotNull() && parameterOfList.OrderByDynamic.Item1.IsValid())
            {
                IQuery = IQuery.OrderByDynamic(parameterOfList.OrderByDynamic.Item1, parameterOfList.OrderByDynamic.Item2);
            }
            else
            {
                if (parameterOfList.OrderBy.IsNotNull())
                {
                    IQuery = parameterOfList.OrderBy(IQuery);
                }
            }

            return IQuery;
        }

        private static IQueryable<T> ConfigureFilter(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Filter.IsNotNull())
            {
                IQuery = IQuery.Where(parameterOfList.Filter);
            }

            if (parameterOfList.WhereDynamic.IsNotNull() && parameterOfList.WhereDynamic.Filters.IsNotNull())
            {
                IQuery = IQuery.WhereDynamic(parameterOfList.WhereDynamic);
            }

            return IQuery;
        }

        private static IQueryable<T> ConfigureInclude(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Include.IsNotNull())
            {
                IQuery = parameterOfList.Include.Aggregate(IQuery,
                                (current, include) => current.Include(include));
            }

            return IQuery;
        }

        private static IQueryable<T> ConfigureMaxCount(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Skip > -1)
            {
                parameterOfList.MaxCount = IQuery.LongCount();
            }

            return IQuery;
        }

        private IQueryable<T> ConfigurePaged(IQueryable<T> IQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Take > 0)
            {
                if (parameterOfList.Skip > -1)
                {
                    IQuery = IQuery.Skip(parameterOfList.Skip);
                }

                IQuery = IQuery.Take(parameterOfList.Take);
            }
            else
            {
                if (parameterOfList.Skip > -1)
                {
                    parameterOfList.Take = Constants.DefaultNumeroDeRegistros;

                    IQuery = IQuery
                        .Skip(parameterOfList.Skip)
                        .Take(parameterOfList.Take);
                }
            }

            return IQuery;
        }
    }
}
