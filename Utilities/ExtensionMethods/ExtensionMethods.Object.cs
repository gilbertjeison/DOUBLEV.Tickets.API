using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
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

        public static void TrimAll<T>(this T entity, params string[] ignoredFields)
        {
            Dictionary<Type, PropertyInfo[]> trimProperties = new Dictionary<Type, PropertyInfo[]>();
            if (!trimProperties.TryGetValue(typeof(T), out PropertyInfo[] props))
            {
                props = typeof(T)
                          .GetProperties()
                          .Where(c => !ignoredFields.Contains(c.Name) &&
                                      c.CanWrite &&
                                      c.PropertyType == typeof(string))
                          .ToArray();

                trimProperties.Add(typeof(T), props);
            }

            foreach (PropertyInfo property in props)
            {
                string value = Convert.ToString(property.GetValue(entity, null), CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(entity, value.Trim(), null);
                }
            }
        }

        public static List<CustomAttribute> GetAttributes<T>()
            where T : class
        {
            PropertyInfo[] property = typeof(T).GetProperties();
            List<CustomAttribute> attributes = new List<CustomAttribute>();
            foreach (PropertyInfo item in property)
            {
                CustomAttribute attribute = new CustomAttribute();
                var displayAttribute = (DisplayAttribute)item.GetCustomAttribute(typeof(DisplayAttribute));
                if (displayAttribute.IsNotNull())
                {
                    attribute.CustomName = displayAttribute.Name;
                    attribute.Name = item.Name.Replace("_", ".");
                    attribute.ObjectType = item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? item.PropertyType.GenericTypeArguments[0].Name : item.PropertyType.Name;
                    attributes.Add(attribute);
                }
            }

            return attributes;
        }
        public class CustomAttribute
        {
            /// <summary>
            /// Nombre customizado del campo.
            /// </summary>
            public string CustomName { get; set; }

            /// <summary>
            /// Nombre original del cmapo.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Tipo del campo.
            /// </summary>
            public string ObjectType { get; set; }
        }

    }
}
