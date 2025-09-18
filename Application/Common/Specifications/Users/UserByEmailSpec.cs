using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications.Users;

public class UserByEmailSpec : BaseSpecification<User>
{
    public UserByEmailSpec(string email, Guid? id = null)
    {
        Query.Where(u => u.Email == email)
            //.Include(u => u.Role)
            ;
        
        if (id.HasValue)
            Query.Where(u => u.Id != id.Value);
    }
}