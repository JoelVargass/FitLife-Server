using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications.Users;


public class ListUsersSpec : BaseSpecification<User>
{
    public ListUsersSpec(int page, string? search, Guid? entityId, Guid? roleId, bool isCount = false)
    {
        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            Query.Where(u =>
                u.Name.ToLower().Contains(lowerSearch) ||
                u.FirstLastName.ToLower().Contains(lowerSearch) ||
                (u.SecondLastName != null && u.SecondLastName.ToLower().Contains(lowerSearch))
            );
        }

        /*
        if (roleId.HasValue)
        {
            Query.Where(u => u.RoleId == roleId);
        }

        if (!isCount)
        {
            Query.Include(u => u.Role).Skip((page - 1) * 10).Take(10).OrderByDescending(x => x.CreateDate);
        }
        */
    }
}