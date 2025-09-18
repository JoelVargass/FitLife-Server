using Domain.Entities;
using Ardalis.Specification;

namespace Application.Common.Specifications.Users;

public class UserByIdSpec : BaseSpecification<User>
{
    public UserByIdSpec(Guid id)
    {
        Query.Where(u => u.Id == id)
            //.Include(u => u.Role)
            ;
    }
}