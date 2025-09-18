using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Application.Common.Results;
using Application.Interfaces.Repositories;
//using Domain.Common.Constants;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private IUserRepository _userRepositoryImplementation;

    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await Context.Users
            //.Include(u => u.Role)

            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ListResult<User>> ListWithRoleAsync(
        int page = 1, 
        int pageSize = 10,
        Expression<Func<User, bool>>? filter = null)
    {
        var query = Context.Users
            //.Include(u => u.Role)
            .AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.FirstLastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new ListResult<User>(
            items,
            totalItems,
            page,
            pageSize,
            (int)Math.Ceiling((double)totalItems / pageSize)
        );
    }

    public async Task<User?> IncludeRoleAsync(Guid id)
    {
        return await Context.Users
            //.Include(u => u.Role)

            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByRecoveryTokenAsync(string recoveryToken)
    {
        return await Context.Users
            .FirstOrDefaultAsync(u => u.RecoveryToken == recoveryToken);
    }

    public async Task<ErrorOr<ListResult<User>>> ListUsersAsync(
        int page = 1,
        int pageSize = 10,
        string? name = null
        //Guid? roleId = null
        )
    {
        var query = Context.Users
            //.Include(u => u.Role)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(u =>
                u.Name.Contains(name) ||
                u.FirstLastName.Contains(name) ||
                (u.SecondLastName != null && u.SecondLastName.Contains(name)));
        }

        /*
        if (roleId.HasValue)
        {
            query = query.Where(u => u.RoleId == roleId.Value);
        }
        */
        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.FirstLastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new ListResult<User>(
            items,
            totalItems,
            page,
            pageSize,
            (int)Math.Ceiling((double)totalItems / pageSize)
        );
    }
    

    public async Task<ListResult<User>> ListAsync(
        int page = 1,
        int pageSize = 10,
        Expression<Func<User, bool>>? filter = null)
    {
        var query = Context.Users
            //.Include(u => u.Role)
            .AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.FirstLastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new ListResult<User>(
            items,
            totalItems,
            page,
            pageSize,
            (int)Math.Ceiling((double)totalItems / pageSize)
        );
    }
    
    public async Task<List<User>> GetUsersByRoleAsync( CancellationToken cancellationToken = default)
    {
        return await Context.Users
            //.Include(u => u.Role)
            //.Where(u => u.RoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<User>> GetUsersByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            //.Include(u => u.Role)
            .Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }

}