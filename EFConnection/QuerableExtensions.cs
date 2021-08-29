using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EFConnection
{
    public enum OrderingEnum
    {
        ASC,
        DESC
    }

    public static class QuerableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string property, OrderingEnum ordering = OrderingEnum.ASC)
        {
            return ApplyOrder<TSource>(source, property, ordering == OrderingEnum.ASC ? "OrderBy" : "OrderByDescending");
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IQueryable<TSource> source, string property, OrderingEnum ordering = OrderingEnum.ASC)
        {
            return ApplyOrder<TSource>(source, property, ordering == OrderingEnum.ASC ? "ThenBy" : "ThenByDescending");
        }

        static IOrderedQueryable<TSource> ApplyOrder<TSource>(IQueryable<TSource> source, string property, string ordering)
        {
            Type type = typeof(TSource);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            PropertyInfo pi = type.GetProperty(property);
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == ordering
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TSource), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<TSource>)result;
        }
    }
}
