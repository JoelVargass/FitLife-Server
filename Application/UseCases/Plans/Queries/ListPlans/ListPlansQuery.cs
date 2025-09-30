using Application.Common.Results;
using Application.UseCases.Plans.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Queries.ListPlans;

public record ListPlansQuery(
    Guid UserId,
    int Page = 1, 
    int PageSize = 10, 
    string? Name = null
) : IRequest<ErrorOr<ListResult<PlanResult>>>;