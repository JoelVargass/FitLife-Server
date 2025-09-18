/*
using Ardalis.Specification;
using Domain.Common.Constants;
using Domain.Entities;

namespace Application.Common.Specifications.Roles;

public class ListedRolesSpec : BaseSpecification<Role>
{
    public ListedRolesSpec(int page, string? search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            Query.Where(r => r.Name.ToLower().Contains(lowerSearch));
        }
        
        Query.Skip((page - 1) * TutoresParConstants.PageSize).Take(TutoresParConstants.PageSize);
    }
} */