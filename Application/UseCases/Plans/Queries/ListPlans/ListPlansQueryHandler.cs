using Application.Common.Results;
using Application.Interfaces.Repositories;
using Application.UseCases.Plans.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Queries.ListPlans;

public class ListPlansQueryHandler : IRequestHandler<ListPlansQuery, ErrorOr<ListResult<PlanResult>>>
{
    private readonly IPlanRepository _planRepository;

    public ListPlansQueryHandler(IPlanRepository repository)
    {
        _planRepository = repository;
    }

    public async Task<ErrorOr<ListResult<PlanResult>>> Handle(ListPlansQuery query, CancellationToken cancellationToken)
    {
        var result = await _planRepository.ListAsync(
            page: query.Page,
            pageSize: query.PageSize,
            filter: c =>
                c.UserId == query.UserId &&
                (string.IsNullOrEmpty(query.Name) || c.Name.Contains(query.Name))
        );

        return ListResult<PlanResult>.From(result, result.Items.Select(c => c.ToResult()));
    }
}