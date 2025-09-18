
/*
using Ardalis.Specification;
using TutoresPar.Domain.Entities;

namespace TutoresPar.Application.Common.Specifications.Roles;

public class CountRolesByTermSpec : BaseSpecification<Role>
{
    public CountRolesByTermSpec(string? search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            Query.Where(r => r.Name.ToLower().Contains(lowerSearch));
        }
    }
}
*/