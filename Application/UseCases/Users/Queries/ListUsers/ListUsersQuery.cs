using ErrorOr;
using MediatR;
using Application.Common.Results;
using Application.UseCases.Users.Common;

namespace Application.UseCases.Users.Queries.ListUsers;

public record ListUsersQuery(
    int Page = 1, 
    int PageSize = 10, 
    string? Name = null
) : IRequest<ErrorOr<ListResult<UserResult>>>;