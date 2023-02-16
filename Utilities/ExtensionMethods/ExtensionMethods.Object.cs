using System.Linq.Expressions;
using Utilities.CustomModels;
using Utilities.ExpressionHelper;

namespace Utilities.ExtensionMethods
{
    public static partial class ExtensionMethods
    {
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> query,
                                                    Filter whereMember)
        {
            if (whereMember.IsNotNull() && whereMember.Filters.IsNotNull())
            {
                return CreateWhereDynamic(query, whereMember);
            }

            return query;
        }

        private static IQueryable<T> CreateWhereDynamic<T>(this IQueryable<T> query,
                                                    Filter whereMember)
        {
            Expression<Func<T, bool>> queryFilter;
            foreach (var itemWhere in whereMember.Filters)
            {
                if (itemWhere.Name.IsValid() &&
                    itemWhere.Values.IsNotNull() &&
                    itemWhere.Values.First().ToString().IsValid())
                {
                    queryFilter = ExpressionHelper.ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                       itemWhere.Operator,
                                                                       itemWhere.Values.First().ToString().Trim());

                    for (int i = 1; i < itemWhere.Values.Length; i++)
                    {
                        if (itemWhere.Values[i].ToString().IsValid())
                        {
                            queryFilter = queryFilter.Or(ExpressionHelper.ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                                              itemWhere.Operator,
                                                                                              itemWhere.Values[i].ToString().Trim()));
                        }
                    }

                    query = query.Where(queryFilter);
                }
            }

            return query;
        }
    }
}
