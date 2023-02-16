using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Utilities.CustomModels;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;

namespace Utilities.ExpressionHelper
{
    public static partial class ExpressionHelper
    {
        public static MethodCallExpression OrderBy<T>(this IQueryable<T> query,
                                                      string orderByMember,
                                                      EnumerationApplication.Orden direction)
        {
            if (query.IsNotNull() && orderByMember.IsValid())
            {
                var queryElementTypeParam = Expression.Parameter(typeof(T));
                var memberAccess = GetMemberExpression(queryElementTypeParam, orderByMember);
                var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

                var orderBy = Expression.Call(
                                            typeof(Queryable),
                                            direction == EnumerationApplication.Orden.Asc ? "OrderBy" : "OrderByDescending",
                                            new[] { typeof(T), memberAccess.Type },
                                            query.Expression,
                                            Expression.Quote(keySelector));

                return orderBy;
            }

            return null;
        }

        public static Expression<Func<T, bool>> GetCriteriaWhere<T>(string fieldName, EnumerationApplication.OperationExpression selectedOperator, object fieldValue)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = GetProperty(props, fieldName, true);

            var parameter = Expression.Parameter(typeof(T));
            var expressionParameter = GetMemberExpression(parameter, fieldName);

            if (prop != null && fieldValue != null)
            {
                BinaryExpression body = null;
                EnumerationApplication.OperationExpression enumOperationExpression = selectedOperator;
                if ((prop.PropertyType == typeof(bool) ||
                    prop.PropertyType == typeof(bool?)) &&
                    selectedOperator != EnumerationApplication.OperationExpression.Equals)
                {
                    enumOperationExpression = EnumerationApplication.OperationExpression.Equals;
                }

                Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                switch (enumOperationExpression)
                {
                    case EnumerationApplication.OperationExpression.Equals:
                        body = Expression.Equal(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);
                    case EnumerationApplication.OperationExpression.NotEquals:
                        body = Expression.NotEqual(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);
                    case EnumerationApplication.OperationExpression.Minor:
                        body = Expression.LessThan(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);
                    case EnumerationApplication.OperationExpression.MinorEquals:
                        body = Expression.LessThanOrEqual(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);
                    case EnumerationApplication.OperationExpression.Mayor:
                        body = Expression.GreaterThan(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);
                    case EnumerationApplication.OperationExpression.MayorEquals:
                        body = Expression.GreaterThanOrEqual(expressionParameter, Expression.Constant(Convert.ChangeType(fieldValue, t), prop.PropertyType));
                        return Expression.Lambda<Func<T, bool>>(body, parameter);

                    case EnumerationApplication.OperationExpression.Like:
                        MethodInfo contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(ExpressionMethodString(prop, fieldValue, expressionParameter, contains),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.NotLike:
                        MethodInfo notContains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Not(ExpressionMethodString(prop, fieldValue, expressionParameter, notContains)),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.StartsWith:
                        MethodInfo startsWith = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(ExpressionMethodString(prop, fieldValue, expressionParameter, startsWith),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.NotStartsWith:
                        MethodInfo notStartsWith = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Not(ExpressionMethodString(prop, fieldValue, expressionParameter, notStartsWith)),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.EndsWith:
                        MethodInfo endsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(ExpressionMethodString(prop, fieldValue, expressionParameter, endsWith),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.NotEndsWith:
                        MethodInfo notEndsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Not(ExpressionMethodString(prop, fieldValue, expressionParameter, notEndsWith)),
                                                                parameter);

                    case EnumerationApplication.OperationExpression.Contains:
                        return Contains<T>(fieldValue, parameter, expressionParameter);

                    default:
                        throw new CustomException(EnumerationMessage.Message.ErrorGeneral, new[] { messageException });
                }
            }
            else
            {
                Expression<Func<T, bool>> filter = x => true;
                return filter;
            }
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, bool>> or)
        {
            if (expr == null)
            {
                return or;
            }
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(new AddExpressionVisitor(expr.Parameters[0], or.Parameters[0]).Visit(expr.Body), or.Body), or.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, bool>> and)
        {
            if (expr == null)
            {
                return and;
            }
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(new AddExpressionVisitor(expr.Parameters[0], and.Parameters[0]).Visit(expr.Body), and.Body), and.Parameters);
        }
    }
}
