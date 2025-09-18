using System.Linq.Expressions;
using Ardalis.Specification;

namespace Application.Common.Specifications;

/*
 * Specification base para todas las entidades
 * hace la validacion para comprobar que la entidad no este eliminada con SoftDelete
 */
public class BaseSpecification<T> : Specification<T> where T : class
{
    public BaseSpecification()
    {
        var parameter = Expression.Parameter(typeof(T), "t");
        var isDeletedProperty = typeof(T).GetProperty("IsDeleted");

        if (isDeletedProperty != null)
        {
            var isDeletedExpression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(parameter, "IsDeleted"),
                    Expression.Constant(false)
                ),
                parameter
            );
            Query.Where(isDeletedExpression);
        }
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria) : this()
    {
        Query.Where(criteria);
    }
}