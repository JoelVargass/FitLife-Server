using System.Linq.Expressions;
using ErrorOr;
using Application.Common.Results;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);

    Task<ListResult<User>> ListWithRoleAsync(
        int page = 1, 
        int pageSize = 10,
        Expression<Func<User, bool>>? filter = null);

    Task<User?> IncludeRoleAsync(Guid id);

    Task<User?> GetByRecoveryTokenAsync(string recoveryToken);

    Task<ErrorOr<ListResult<User>>> ListUsersAsync(
        int page = 1,
        int pageSize = 10,
        string? name = null
        );
    
    Task<List<User>> GetUsersByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
    
}