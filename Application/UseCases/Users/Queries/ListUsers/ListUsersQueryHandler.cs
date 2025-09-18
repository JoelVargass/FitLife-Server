using ErrorOr;
using MediatR;
using Application.Common.Results;
using Application.Interfaces.Repositories;
using Application.UseCases.Users.Common;

namespace Application.UseCases.Users.Queries.ListUsers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, ErrorOr<ListResult<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryHandler(IUserRepository repository)
    {
        _userRepository = repository;
    }

    public async Task<ErrorOr<ListResult<UserResult>>> Handle(ListUsersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _userRepository.ListAsync(page: query.Page, pageSize: query.PageSize,
            !string.IsNullOrEmpty(query.Name) ? c => c.Name.Contains(query.Name) : null);

        return ListResult<UserResult>.From(result, result.Items.Select(c => c.ToResult()));
    }
}