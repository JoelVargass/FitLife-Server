/*
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(TutoresParDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await Context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}
*/