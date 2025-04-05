using StoreCenter.Domain.Dtos;
using System.Linq.Expressions;

namespace StoreCenter.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        // The purpose of this extension method is to apply pagination, sorting, and searching to an IQueryable collection.
        // It takes a queryable collection of type T and a PaginationOptions object as parameters.
        // Here T is a generic type parameter representing the type of entities in the collection. For example, it could be a collection of categories, products, etc.
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationOptions options)
        {
            
            if (!string.IsNullOrWhiteSpace(options.SearchTerm) && !string.IsNullOrWhiteSpace(options.SearchField))
            {
                var property = typeof(T).GetProperty(options.SearchField);
                if (property != null && property.PropertyType == typeof(string))
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var propertyExpression = Expression.Property(parameter, options.SearchField);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var searchTerm = Expression.Constant(options.SearchTerm);
                    var containsExpression = Expression.Call(propertyExpression, containsMethod!, searchTerm);
                    var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
                    query = query.Where(lambda);
                }
            }

            // IQueryable<T> query is used to represent a collection of entities that can be queried using LINQ (Language Integrated Query).
            // query = query.Where(x => x.Name.Contains(options.SearchTerm) || x.Description.Contains(options.SearchTerm));
            query = query.OrderByDynamic(options.OrderBy, options.IsDescending);

            // Apply pagination
            return query.Skip((options.PageNumber - 1) * options.PageSize)
                        .Take(options.PageSize);
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByProperty, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, orderByProperty);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = descending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { query, lambda })!;
        }
    }


}
