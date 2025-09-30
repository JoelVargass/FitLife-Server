using Application.Common.Results;
using Application.Interfaces.Repositories;
using Application.UseCases.Exercises.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Queries.ListExercises;

public class ListExercisesQueryHandler : IRequestHandler<ListExercisesQuery, ErrorOr<ListResult<ExerciseResult>>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public ListExercisesQueryHandler(IExerciseRepository repository)
    {
        _exerciseRepository = repository;
    }

    public async Task<ErrorOr<ListResult<ExerciseResult>>> Handle(ListExercisesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _exerciseRepository.ListAsync(
            page: query.Page,
            pageSize: query.PageSize,
            filter: c =>
                c.UserId == query.UserId &&
                (string.IsNullOrEmpty(query.Name) || c.Name.Contains(query.Name)));
        return ListResult<ExerciseResult>.From(result, result.Items.Select(c => c.ToResult()));
    }
}