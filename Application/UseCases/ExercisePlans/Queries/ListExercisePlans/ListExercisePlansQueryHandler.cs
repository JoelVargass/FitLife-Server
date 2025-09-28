using Application.Common.Results;
using Application.Interfaces.Repositories;
using Application.UseCases.ExercisePlans.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.ExercisePlans.Queries.ListExercisePlans;

public class ListExercisePlansQueryHandler : IRequestHandler<ListExercisePlansQuery, ErrorOr<ListResult<ExercisePlanResult>>>
{
    private readonly IExercisePlanRepository _exercisePlanRepository;

    public ListExercisePlansQueryHandler(IExercisePlanRepository repository)
    {
        _exercisePlanRepository = repository;
    }

    public async Task<ErrorOr<ListResult<ExercisePlanResult>>> Handle(ListExercisePlansQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _exercisePlanRepository.ListAsync(
            page: query.Page, 
            pageSize: query.PageSize
        );

        return ListResult<ExercisePlanResult>.From(
            result, 
            result.Items.Select(c => c.ToResult())
        );
    }
}