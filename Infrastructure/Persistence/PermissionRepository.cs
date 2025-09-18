/*
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence;

public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Permission>> GetByRoleAsync(Guid roleId)
    {
        return await Context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Include(rp => rp.Permission)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}
*/