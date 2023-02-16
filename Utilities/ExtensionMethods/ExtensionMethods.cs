using System.Globalization;
using Utilities.Enumerations;

namespace Utilities.ExtensionMethods
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ValidatedNotNullAttribute : Attribute { }

    public static partial class ExtensionMethods
    {
        public static bool IsNotNull<T>([ValidatedNotNullAttribute] this T valid) where T : class => valid != null;
        
        public static bool IsValid([ValidatedNotNull] this string s)
        {
            return (s.IsNotNull() && s.Trim().Length > 0);
        }

        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0], CultureInfo.CurrentCulture) + s[1..].ToLower(CultureInfo.CurrentCulture);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query,
                                              string orderByMember,
                                              EnumerationApplication.Orden direction)
        {
            var OrderBy = ExpressionHelper.ExpressionHelper.OrderBy(query, orderByMember, direction);
            if (OrderBy.IsNotNull())
            {
                return query.Provider.CreateQuery<T>(OrderBy);
            }

            return query;
        }
    }
}
